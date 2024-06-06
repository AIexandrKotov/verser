using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace verser
{
    public static class Program
    {
        #region Arguments
        public static string ReplaceAll(this string str, IDictionary<string, string> replacer)
        {
            foreach (var x in replacer)
                str = str.Replace(x.Key, x.Value);
            return str;
        }
        public static bool HasArgument(this string[] arguments, string arg, Dictionary<string, string> shorts = null)
            => arguments.Contains(shorts == null ? arg : arg.ReplaceAll(shorts));
        public static bool TryGetArgument(this string[] arguments, string arg, out string value, Func<string> not_found = null, Dictionary<string, string> shorts = null, int offset = 0)
        {
            if (shorts != null) arg = arg.ReplaceAll(shorts);
            value = null;
            var ind = Array.FindIndex(arguments, x => x == arg);

            if (ind == -1) return false;
            if (arguments.Length > ind + 1 + offset && !arguments[ind + 1 + offset].StartsWith("--"))
            {
                value = arguments[ind + 1 + offset];
                return true;
            }
            else
            {
                if (not_found == null) return false;
                else
                {
                    value = not_found();
                    return true;
                }
            }
        }
        public static string GetArgument(this string[] arguments, string arg, Dictionary<string, string> shorts = null, int offset = 0)
            => arguments.TryGetArgument(arg, out var value, null, shorts, offset) ? value : null;
        public static string[] Splitter(this string str)
        {
            var ret = new List<string>();
            var current = new StringBuilder();

            const int state_whitespace = 0;
            const int state_any = 1;
            const int state_string = 2;

            var state = state_whitespace;

            foreach (var c in str)
            {
                if (state == state_string)
                {
                    if (c == '"')
                    {
                        ret.Add(current.ToString());
                        current.Clear();
                        state = state_whitespace;
                    }
                    else
                    {
                        current.Append(c);
                        continue;
                    }
                }
                else
                {
                    if (char.IsWhiteSpace(c))
                    {
                        if (state == state_whitespace) continue;
                        else
                        {
                            ret.Add(current.ToString());
                            current.Clear();
                            state = state_whitespace;
                        }
                    }
                    else
                    {
                        if (c == '"')
                        {
                            if (state == state_whitespace)
                            {
                                state = state_string;
                                continue;
                            }
                            else current.Append(c);
                        }
                        else
                        {
                            if (state == state_whitespace)
                            {
                                state = state_any;
                                current.Append(c);
                                continue;
                            }
                            else current.Append(c);
                        }
                    }
                }
            }

            if (current.Length > 0) ret.Add(current.ToString());

            return ret.ToArray();
        }
        #endregion

        private static Assembly verserAssembly = Assembly.GetExecutingAssembly();
        private static string VerserHelp =
@"----- VERSER COMMANDS -----
    // <PON> is PathOrName string

    -q --quit               - Quit VERSER
    -l --projects           - Outs list of projects
    -a --add <PON>          - Register project
         --empty            - Don't insert default configurations into project
    -r --remove <PON>       - Unregister project
    -t --trace <PON>        - Add versioning for project
    -u --untrace <PON>      - Remove versioning for project
    -s --switch <PON> <STR> - Switch project config to another
       --append <PON>       - Append version of project based on config
    -M --major <PON>        - Append Major version of project
    -m --minor <PON>        - Append Minor version of project
    -p --patch <PON>        - Append Patch version of project

       --clear     - Clear screen
    -h --help      - Outs this text
";
        /*
        
    -c --config <...>
         --out or 0 args    - Outs stored config
         --out <STR>        - Outs template config with name STR
         --out <PON> <STR>  - Outs 
         --new <STR>        - Store new config with name STR
         --copy <PON> <STR> - Store in cache copied config of project
         --copy <STR>       - Store in cache copied template 
         --add <PON>        - Add config into project
         --add              - Add config into template configurations
         //add replace config if name is exists
         --set <STR> <VAL>  - Set property of config to value
        */
        public static Dictionary<string, string> ShortVerserCommands = new Dictionary<string, string>()
        {
            { "-h", "--help" },
            { "-q", "--quit" },
            { "-l", "--projects" },
            { "-a", "--add" },
            { "-r", "--remove" },
            { "-t", "--trace" },
            { "-u", "--untrace" },
            { "-s", "--switch" },
            { "-M", "--major" },
            { "-m", "--minor" },
            { "-P", "--patch" },
        };

        public static void OutHelp()
        {
            Console.Write(VerserHelp);
        }

        public static void Initialize()
        {
            VerserAPI.Updated += (canceled) =>
            {
                OutAsWarning($"{Path.GetFileName(VerserAPI.ConfigPath)} has been updated." + (canceled ? " The command has been canceled." : ""));
            };
            var readed = VerserAPI.ReadVerserConfig();
            if (readed != null)
            {
                Console.WriteLine(readed.ToString());
                Environment.Exit(-1);
            }
        }

        public static ConsoleColor GetColorForConfig(Config config)
        {
            if (config.ConfigName.Contains("ReleaseCandidate"))
                return ConsoleColor.Yellow;
            else if (config.ConfigName.Contains("Release"))
                return ConsoleColor.Green;
            else if (config.ConfigName.Contains("Beta"))
                return ConsoleColor.DarkYellow;
            else if (config.ConfigName.Contains("Alpha"))
                return ConsoleColor.Red;
            return ConsoleColor.White;
        }

        public static (string, ProjectInfo[])[] GetProjectsByName() => VerserAPI.GetProjects().GroupBy(x => x.Name).OrderBy(x => x.Key).Select(x => (x.Key, x.Select(p => VerserAPI.GetProjectInfo(p)).ToArray())).ToArray();

        public static void OutProjects()
        {
            var projects = GetProjectsByName();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("-- Verser assigned projects --");
            Console.ResetColor();

            void AddOfProj(ProjectInfo project)
            {
                Console.Write(" ");
                Console.ForegroundColor = project.IsTracing ? ConsoleColor.Green : ConsoleColor.Red;
                Console.Write(project.IsTracing ? "--trace " : "--untrace ");
                Console.ResetColor();
                var configcolor = Console.ForegroundColor = project.Config == null ? ConsoleColor.White : GetColorForConfig(project.Config);
                Console.Write($"{project.Config?.ConfigName ?? "-----"} ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{project.Version}");
                Console.ResetColor();
                Console.Write(" -> ");
                Console.ForegroundColor = configcolor;
                Console.Write((project.Config == null || !project.IsTracing) ? project.Version : project.Version.Next(project.Config));
                Console.WriteLine();
                Console.ResetColor();
            }

            foreach (var projectname in projects)
            {
                Console.Write("    ");
                if (projectname.Item2.Length == 1)
                {
                    var project = projectname.Item2[0];
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(projectname.Item1);
                    AddOfProj(project);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(projectname.Item1);
                    foreach (var project in projectname.Item2)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write($"       at {project.Project.Path}");
                        AddOfProj(project);
                    }
                }
            }
        }

        public static void OutAsException(string s)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(s);
            Console.ResetColor();
        }
        public static void OutAsWarning(string s)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(s);
            Console.ResetColor();
        }

        public static void OutHead()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Verser ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(VerserAPI.GetVersionOfAssembly(verserAssembly));
            Console.ResetColor();
            Console.Write("input");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" -h ");
            Console.ResetColor();
            Console.WriteLine("for help");
        }

        public static void RunVerserConsole()
        {
            Initialize();
            OutHead();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(">>> ");
                var arguments = Splitter(Console.ReadLine().Trim());
                arguments = Array.ConvertAll(arguments, x => x.StartsWith("-") && !x.StartsWith("--") ? x.ReplaceAll(ShortVerserCommands) : x);

                try
                {
                    if (arguments.HasArgument("-q", ShortVerserCommands)) return;
                    else if (arguments.HasArgument("-h", ShortVerserCommands))
                        OutHelp();
                    else if (arguments.HasArgument("-l", ShortVerserCommands))
                        OutProjects();
                    else if (arguments.HasArgument("--clear"))
                    {
                        Console.Clear(); OutHead();
                    }
                    else if (arguments.TryGetArgument("-t", out var pon, null, ShortVerserCommands))
                        VerserAPI.Trace(pon);
                    else if (arguments.TryGetArgument("-u", out pon, null, ShortVerserCommands))
                        VerserAPI.Untrace(pon);
                    else if (arguments.TryGetArgument("-a", out pon, null, ShortVerserCommands))
                        VerserAPI.AddProject(pon, !arguments.HasArgument("--empty", ShortVerserCommands));
                    else if (arguments.TryGetArgument("-r", out pon, null, ShortVerserCommands))
                        VerserAPI.RemoveProject(pon);
                    else if (arguments.TryGetArgument("-s", out pon, null, ShortVerserCommands) && arguments.TryGetArgument("-s", out var config, null, ShortVerserCommands, 1))
                        VerserAPI.SwitchToConfig(pon, config);
                    else if (arguments.TryGetArgument("-M", out pon, null, ShortVerserCommands))
                        VerserAPI.Major(pon);
                    else if (arguments.TryGetArgument("-m", out pon, null, ShortVerserCommands))
                        VerserAPI.Minor(pon);
                    else if (arguments.TryGetArgument("-p", out pon, null, ShortVerserCommands))
                        VerserAPI.Patch(pon);
                    else if (arguments.TryGetArgument("--append", out pon))
                        VerserAPI.Append(pon);
                    else OutAsWarning("The command was not recognized");
                }
                catch (Exception e)
                {
                    OutAsException(e.ToString());
                }
                Console.ResetColor();
            }
        }

        public static void Main(string[] args)
        {
            Environment.CurrentDirectory = Path.GetDirectoryName(VerserAPI.VerserExePath);
            if (args.Length == 0) RunVerserConsole();
            else if (args.TryGetArgument("--append", out var pon))
            {
                Initialize();
                VerserAPI.Append(pon, true);
            }
            else if (args.TryGetArgument("--major", out pon))
            {
                Initialize();
                VerserAPI.Major(pon, true);
            }
            else if (args.TryGetArgument("--minor", out pon))
            {
                Initialize();
                VerserAPI.Minor(pon, true);
            }
            else if (args.TryGetArgument("--patch", out pon))
            {
                Initialize();
                VerserAPI.Patch(pon, true);
            }
        }
    }
}

using System;
using System.Collections.Generic;
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
    -p --projects           - Outs list of projects
       --addproject <pon>  - Register project
         --empty            - Don't insert default configurations into projectname
       --remproject <pon>  - Unregister project
       --trace <PON>        - Add versioning for projectname
       --untrace <PON>      - Remove versioning for projectname
       --switch <PON> <STR> - Switch project config to another
       --append <PON>       - Append version of project

    -h --help      - Outs this text
"; 
        public static Dictionary<string, string> ShortVerserCommands = new Dictionary<string, string>()
        {
            { "-h", "--help" },
            { "-q", "--quit" },
            { "-p", "--projects" },
        };

        public static void OutHelp()
        {
            Console.Write(VerserHelp);
        }

        public static void Initialize()
        {
            var readed = VerserAPI.ReadVerserConfig();
            if (readed is FileNotFoundException)
            {
                VerserAPI.InitDefaultConfig();
            }
            else if (readed != null)
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
                return ConsoleColor.Magenta;
            else if (config.ConfigName.Contains("Alpha"))
                return ConsoleColor.Red;
            return ConsoleColor.White;
        }

        public static void OutProjects()
        {
            var projects = VerserAPI.GetProjects().GroupBy(x => x.Name).OrderBy(x => x.Key).Select(x => (x.Key, x.Select(p => VerserAPI.GetProjectInfo(p)).ToArray())).ToArray();
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
                    Console.Write(projectname.Key);
                    AddOfProj(project);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(projectname.Key);
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

        public static void RunVerserConsole()
        {
            Initialize();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Verser ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(verserAssembly.GetName().Version.ToString(3));
            Console.ResetColor();
            Console.Write("input");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" -h ");
            Console.ResetColor();
            Console.WriteLine("for help");
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
                    else if (arguments.HasArgument("-p", ShortVerserCommands))
                        OutProjects();
                    else if (arguments.TryGetArgument("--trace", out var pon))
                        VerserAPI.Trace(pon);
                    else if (arguments.TryGetArgument("--untrace", out pon))
                        VerserAPI.Untrace(pon);
                    else if (arguments.TryGetArgument("--addproject", out pon))
                        VerserAPI.AddProject(pon, !arguments.HasArgument("--empty", ShortVerserCommands));
                    else if (arguments.TryGetArgument("--remproject", out pon))
                        VerserAPI.RemoveProject(pon);
                    else if (arguments.TryGetArgument("--switch", out pon) && arguments.TryGetArgument("--switch", out var config, null, null, 1))
                        VerserAPI.SwitchToConfig(pon, config);
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
            else if (args.Length == 2 && args.TryGetArgument("--append", out var pon))
            {
                Initialize();
                VerserAPI.Append(pon);
            }
        }
    }
}

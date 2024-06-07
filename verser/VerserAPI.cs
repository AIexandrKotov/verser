using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace verser
{
    public static class VerserAPI
    {
        public static readonly string ConfigPath;
        public static readonly string CachePath;
        public static readonly string VerserExePath;
        public static readonly string VerserStylesPath;
        public static readonly VerserStyles Style;
        static VerserAPI()
        {
            var directory = Path.GetDirectoryName(VerserExePath = System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);
            ConfigPath = Path.Combine(directory, "verserconfig.xml");
            CachePath = Path.Combine(directory, "verser.cachefile");
            VerserStylesPath = Path.Combine(directory, "verserstyles.xml");
            Style = File.Exists(VerserStylesPath) ? VerserStyles.FromXML(File.ReadAllText(VerserStylesPath)) : VerserStyles.GetDefault();
        }
        private static VerserConfig Config;
        private static DateTime LastConfigWrite;

        private static Cache Cache;
        private static bool UpdateConfig(bool operation_canceled)
        {
            if (LastConfigWrite != File.GetLastWriteTime(ConfigPath))
            {
                var readed = ReadVerserConfig();
                if (readed != null)
                    throw readed;
                Updated(operation_canceled);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Argument if changes applied
        /// </summary>
        public static event Action<bool> Updated = delegate { };

        private static Exception ReadVerserCache()
        {
            try
            {
                Cache = Cache.FromXML(File.ReadAllText(CachePath));
                return null;
            }
            catch (FileNotFoundException)
            {
                Cache = new Cache() { LockMultiplatforms = new Dictionary<string, int>() };
                return null;
            }
            catch (Exception e)
            {
                return e;
            }
        }
        private static Exception SaveVerserCache()
        {
            try
            {
                if (Cache == null) throw new NullReferenceException("Cache was null");
                File.WriteAllText(CachePath, Cache.GetXML());
                return null;
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public static List<Config> GetDefaultTemplateConfigurations()
        {
            var alpha = new Config()
            {
                ConfigName = "Alpha",

                Suffix = "alpha",
                AppendPatch = false,
                AppendPrerelease = true,
                Prerelease = verser.Config.PrereleaseType.Number,
            };
            var beta = new Config()
            {
                ConfigName = "Beta",

                Suffix = "beta",
                AppendPatch = false,
                AppendPrerelease = true,
                Prerelease = verser.Config.PrereleaseType.Number,
            };
            var releasecandidate = new Config()
            {
                ConfigName = "ReleaseCandidate",

                Suffix = "rc",
                AppendPatch = false,
                AppendPrerelease = true,
                Prerelease = verser.Config.PrereleaseType.Number,
            };
            var release = new Config()
            {
                ConfigName = "Release",

                Suffix = "",
                AppendPatch = true,
                AppendPrerelease = false,
                Prerelease = verser.Config.PrereleaseType.None,
            };
            return new List<Config>() { alpha,  beta, releasecandidate, release };
        }
        public static Exception ReadVerserConfig()
        {
            try
            {
                Config = VerserConfig.FromXML(File.ReadAllText(ConfigPath));
                LastConfigWrite = File.GetLastWriteTime(ConfigPath);
                return null;
            }
            catch (FileNotFoundException)
            {
                InitDefaultConfig();
                return null;
            }
            catch (Exception e) 
            {
                return e;
            }
        }
        public static Exception SaveVerserConfig()
        {
            try
            {
                if (Config == null) throw new NullReferenceException("Config was null");
                File.WriteAllText(ConfigPath, Config.GetXML());
                LastConfigWrite = File.GetLastWriteTime(ConfigPath);
                return null;
            }
            catch (Exception e)
            {
                return e;
            }
        }
        private static void InitDefaultConfig()
        {
            Config = new VerserConfig()
            {
                Projects = new List<Project>(),
                TemplateConfigs = GetDefaultTemplateConfigurations(),
            };
            SaveVerserConfig();
        }

        public static void AddProject(string path, bool insert_configs = true)
        {
            if (UpdateConfig(true)) return;
            var project = new Project() { 
                Path = path, 
                Configurations = insert_configs ? Config.TemplateConfigs.Select(x => (Config)x.Clone()).ToList() : new List<Config>(),
            };
            if (!File.Exists(project.Path)) throw new FileNotFoundException("", project.Path);
            if (Path.GetExtension(project.Path) != ".csproj") throw new NotSupportedException($"{project.Path} is not .csproj file");
            Config.Projects.Add(project);
            SaveVerserConfig();
        }

        public static Project GetProjectByPathOrName(string path_or_name)
        {
            UpdateConfig(false);
            var pathfinded = Config.Projects.Where(x => x.Path == path_or_name).ToArray();
            if (pathfinded.Length != 0)
                return pathfinded[0];
            var namefinded = Config.Projects.Where(x => x.Name == path_or_name).ToArray();
            if (namefinded.Length == 1)
                return namefinded[0];
            if (namefinded.Length > 1)
                throw new NotSupportedException($"Several projects have been found with name \"{path_or_name}\"");
            throw new NotSupportedException($"No project named \"{path_or_name}\" was found");
        }
        public static void RemoveProject(string path_or_name)
        {
            if (UpdateConfig(true)) return;
            var project = GetProjectByPathOrName(path_or_name);
            Config.Projects.Remove(project);
            SaveVerserConfig();
        }

        public static Project[] GetProjects()
        {
            UpdateConfig(false);
            return Config.Projects.ToArray();
        }
        public static string GetRelativePath(string relativeTo, string path)
        {
            var sourceParts = Path.GetDirectoryName(Path.GetFullPath(relativeTo).Replace('/', '\\')).Split(Path.DirectorySeparatorChar);
            var targetParts = Path.GetFullPath(path).Replace('/', '\\').Split(Path.DirectorySeparatorChar);

            var n = 0;
            while (n < Math.Min(sourceParts.Length, targetParts.Length))
            {
                if (!string.Equals(sourceParts[n], targetParts[n], StringComparison.CurrentCultureIgnoreCase))
                    break;
                n += 1;
            }

            if (n == 0) throw new NotSupportedException("Files must be on the same volume");
            var relativePath = string.Join("", Enumerable.Repeat(".." + Path.DirectorySeparatorChar, sourceParts.Length - n));
            if (n <= targetParts.Length)
                relativePath += string.Join(Path.DirectorySeparatorChar.ToString(), targetParts.Skip(n));

            return string.IsNullOrWhiteSpace(relativePath) ? "." : relativePath;
        }

        #region Private for tracing
        private static string GetCommandPart() => $"{Path.GetFileName(VerserExePath)}\" --append";
        private static XElement GetTarget(XDocument xdoc, bool append = true)
        {
            var target = xdoc.Root.Elements("Target").FirstOrDefault(x => x.Attribute("Name")?.Value == "PostBuild" && x.Attribute("AfterTargets")?.Value == "PostBuildEvent");
            if (append && target == null)
            {
                target = new XElement("Target");
                target.Add(new XAttribute("Name", "PostBuild"));
                target.Add(new XAttribute("AfterTargets", "PostBuildEvent"));
                xdoc.Root.Add(target);
            }
            return target;
        }
        private static void AddTracer(XElement target, Project project)
        {
            var pathToExe = GetRelativePath(project.Path, VerserExePath);
            var runCommand = $"\"{pathToExe}\" --append \"{project.Path}\"";

            var tracer = new XElement("Exec");
            tracer.Add(new XAttribute("Command", runCommand));
            target.Add(tracer);
        }
        private static void RemoveTracers(XElement target)
        {
            var execs = target.Elements("Exec").ToArray();
            execs.Where(InternalIsTracer).ToList().ForEach(x => x.Remove());
        }
        private static bool IsTracing(XElement target)
        {
            var execs = target.Elements("Exec").ToArray();
            return execs.Any(InternalIsTracer);
        }
        private static bool InternalIsTracer(XElement exec)
        {
            var command = exec.Attribute("Command");
            if (command == null) return false;

            if (command.Value.Contains(GetCommandPart()))
                return true;

            return false;
        }
        #endregion

        #region Private for configuration
        private static XElement GetPropertyGroup(XDocument xdoc)
        {
            var pg = xdoc.Root.Elements("PropertyGroup").LastOrDefault(x => x.Element("Version") != null);
            if (pg == null)
                pg = xdoc.Root.Element("PropertyGroup");
            if (pg == null)
            {
                pg = new XElement("PropertyGroup");
                xdoc.Root.Add(pg);
            }
            return pg;
        }
        private static XElement GetXVersion(XElement propertygroup)
        {
            var xversion = propertygroup.Element("Version");
            if (xversion == null)
            {
                xversion = new XElement("Version");
                xversion.SetValue("0.1.0");
                propertygroup.Add(xversion);
            }
            return xversion;
        }
        private static int GetPlatfromsCount(XElement propertygroup)
        {
            var tf = propertygroup.Element("TargetFrameworks");
            if (tf == null) return 1;
            return tf.Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }
        #endregion

        public static void Trace(string path_or_name)
        {
            if (UpdateConfig(true)) return;
            var project = GetProjectByPathOrName(path_or_name);

            var xdoc = XDocument.Load(project.Path);
            var target = GetTarget(xdoc);

            RemoveTracers(target);
            AddTracer(target, project);

            xdoc.Save(project.Path);
        }
        public static void Untrace(string path_or_name)
        {
            if (UpdateConfig(true)) return;
            var project = GetProjectByPathOrName(path_or_name);

            var xdoc = XDocument.Load(project.Path);
            var target = GetTarget(xdoc);

            RemoveTracers(target);

            xdoc.Save(project.Path);
        }
        public static void SwitchToConfig(string path_or_name, string config_name)
        {
            if (UpdateConfig(true)) return;
            var project = GetProjectInfo(path_or_name);
            var newversion = project.Version.UpdateTo(project.Project.Configurations.FirstOrDefault(x => x.ConfigName == config_name));
            UpdateVersion(project.Project, newversion);
        }

        private static bool LockMultiplatform(ProjectInfo project)
        {
            ReadVerserCache();
            if (Cache.LockMultiplatforms.TryGetValue(project.Project.Path, out var locks))
            {
                if (locks != 0)
                {
                    Cache.LockMultiplatforms[project.Project.Path] = locks - 1;
                    SaveVerserCache();
                    return true;
                }
                else
                {
                    if (project.Platforms > 1)
                    {
                        Cache.LockMultiplatforms[project.Project.Path] = project.Platforms - 1;
                        SaveVerserCache();
                        return false;
                    }
                }
            }
            else
            {
                if (project.Platforms > 1)
                {
                    Cache.LockMultiplatforms[project.Project.Path] = project.Platforms - 1;
                    SaveVerserCache();
                    return false;
                }
            }
            return false;
        }
        public static void Append(string path_or_name, bool lockmultiplatform = false)
        {
            if (UpdateConfig(true)) return;
            var project = GetProjectInfo(path_or_name);
            var newversion = project.Version.Next(project.Config);
            if (!lockmultiplatform || !LockMultiplatform(project))
                UpdateVersion(project.Project, newversion);
        }
        public static void Major(string path_or_name, bool lockmultiplatform = false)
        {
            if (UpdateConfig(true)) return;
            var project = GetProjectInfo(path_or_name);
            var newversion = project.Version.AppendMajor();
            if (!lockmultiplatform || !LockMultiplatform(project))
                UpdateVersion(project.Project, newversion);
        }
        public static void Minor(string path_or_name, bool lockmultiplatform = false)
        {
            if (UpdateConfig(true)) return;
            var project = GetProjectInfo(path_or_name);
            var newversion = project.Version.AppendMinor();
            if (!lockmultiplatform || !LockMultiplatform(project))
                UpdateVersion(project.Project, newversion);
        }
        public static void Patch(string path_or_name, bool lockmultiplatform = false)
        {
            if (UpdateConfig(true)) return;
            var project = GetProjectInfo(path_or_name);
            var newversion = project.Version.AppendPatch();
            if (!lockmultiplatform || !LockMultiplatform(project))
                UpdateVersion(project.Project, newversion);
        }

        public static VerserVersion GetVersionOfAssembly(Assembly assembly)
        {
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return new VerserVersion(fileVersionInfo.ProductVersion);
        }

        public static ProjectInfo GetProjectInfo(string path_or_name)
        {
            return GetProjectInfo(GetProjectByPathOrName(path_or_name));
        }

        public static Config FindConfiguration(VerserVersion version, IEnumerable<Config> configs)
        {
            if (!configs.Any()) return null;
            if (string.IsNullOrEmpty(version.Suffix) && version.Prerelease == null)
                return configs.FirstOrDefault(x => string.IsNullOrEmpty(x.Suffix) && x.Prerelease == verser.Config.PrereleaseType.None);
            if (string.IsNullOrEmpty(version.Suffix) && version.Prerelease != null)
                return configs.FirstOrDefault(x => string.IsNullOrEmpty(x.Suffix) && x.Prerelease != verser.Config.PrereleaseType.None);
            if (!string.IsNullOrEmpty(version.Suffix) && version.Prerelease == null)
                return configs.FirstOrDefault(x => x.Suffix == version.Suffix && x.Prerelease == verser.Config.PrereleaseType.None);
            if (!string.IsNullOrEmpty(version.Suffix) && version.Prerelease != null)
                return configs.FirstOrDefault(x => x.Suffix == version.Suffix && x.Prerelease != verser.Config.PrereleaseType.None);
            return null;
        }

        public static ProjectInfo GetProjectInfo(Project project)
        {
            var xdoc = XDocument.Load(project.Path);
            var target = GetTarget(xdoc);
            var propertygroup = GetPropertyGroup(xdoc);
            var projectInfo_platforms = GetPlatfromsCount(propertygroup);
            var projectInfo_version = new VerserVersion(GetXVersion(propertygroup).Value);
            var projectInfo_is_tracing = IsTracing(target);

            return new ProjectInfo()
            {
                Project = project,
                IsTracing = projectInfo_is_tracing,
                Version = projectInfo_version,
                Platforms = projectInfo_platforms,
                Config = FindConfiguration(projectInfo_version, project.Configurations),
            };
        }
        public static void UpdateVersion(Project project, VerserVersion version)
        {
            var xdoc = XDocument.Load(project.Path);
            var pg = GetPropertyGroup(xdoc);
            var xversion = GetXVersion(pg);
            xversion.SetValue(version.ToString());

            xdoc.Save(project.Path);
        }

        #region Future
        private static VerserConfig InitSearchedEnvironment()
        {
            throw new NotSupportedException();
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace verser
{
    public sealed class Project
    {
        public string Path;
        public List<Config> Configurations;

        public string Name => System.IO.Path.GetFileNameWithoutExtension(Path);

        public XElement GetXML()
        {
            var xproject = new XElement("Project");
            xproject.Add(new XAttribute("path", Path));
            foreach (var config in Configurations)
                xproject.Add(config.GetXML());
            return xproject;
        }
        public static Project FromXML(XElement xproject)
        {
            return new Project()
            {
                Path = xproject.Attribute("path").Value,
                Configurations = xproject.Elements().Select(x => Config.FromXML(x)).ToList(),
            };
        }
    }
    public sealed class Config : ICloneable
    {
        public string ConfigName = "New config";

        //Naming:
        public string Suffix = string.Empty;
        public PrereleaseType Prerelease = PrereleaseType.None;

        public bool AppendPatch = false;
        public bool AppendPrerelease = false;
        public bool ResetPrerelease = true;

        public enum PrereleaseType
        {
            None,
            Number,     //0099
            Date,       //20240603
            DateTime,   //202406030013
        }
        //MAJOR.MINOR.PATCH[-SUFFIX[.Prerelease]]

        public XElement GetXML()
        {
            var xconfig = new XElement("Config");
            xconfig.Add(new XAttribute(nameof(ConfigName), ConfigName));
            xconfig.Add(new XAttribute(nameof(Suffix), Suffix));
            xconfig.Add(new XAttribute(nameof(Prerelease), Prerelease));
            xconfig.Add(new XAttribute(nameof(AppendPatch), AppendPatch));
            xconfig.Add(new XAttribute(nameof(AppendPrerelease), AppendPrerelease));
            xconfig.Add(new XAttribute(nameof(ResetPrerelease), ResetPrerelease));
            return xconfig;
        }
        public static Config FromXML(XElement xconfig)
        {
            return new Config()
            {
                ConfigName = xconfig.Attribute(nameof(ConfigName)).Value,
                Suffix = xconfig.Attribute(nameof(Suffix)).Value,
                Prerelease = (PrereleaseType)Enum.Parse(typeof(PrereleaseType), xconfig.Attribute(nameof(Prerelease)).Value),
                AppendPatch = bool.Parse(xconfig.Attribute(nameof(AppendPatch)).Value),
                AppendPrerelease = bool.Parse(xconfig.Attribute(nameof(AppendPrerelease)).Value),
                ResetPrerelease = bool.Parse(xconfig.Attribute(nameof(ResetPrerelease)).Value),
            };
        }

        public object Clone()
        {
            return new Config()
            {
                AppendPatch = AppendPatch,
                AppendPrerelease = AppendPrerelease,
                ResetPrerelease = ResetPrerelease,
                ConfigName = ConfigName,
                Suffix = Suffix,
                Prerelease = Prerelease,
            };
        }
    }
    internal sealed class VerserConfig
    {
        //Saves to verser.config

        public List<Project> Projects = new List<Project>();
        public List<Config> TemplateConfigs = new List<Config>();

        public string GetXML()
        {
            var xdoc = new XDocument();
            var xroot = new XElement("Verser");
            xdoc.Add(xroot);

            foreach (var config in TemplateConfigs)
                xroot.Add(config.GetXML());
            foreach (var project in Projects)
                xroot.Add(project.GetXML());

            return xdoc.ToString();
        }
        public static VerserConfig FromXML(string str)
        {
            var xdoc = XDocument.Parse(str);
            return new VerserConfig()
            {
                TemplateConfigs = xdoc.Root.Elements("Config").Select(x => Config.FromXML(x)).ToList(),
                Projects = xdoc.Root.Elements("Project").Select(x => Project.FromXML(x)).ToList(),
            };
        }
    }

    public sealed class Cache
    {
        public Dictionary<string, int> LockMultiplatforms;

        public static Cache FromXML(string xml)
        {
            var xdoc = XDocument.Parse(xml);
            
            return new Cache()
            {
                LockMultiplatforms = xdoc.Root.Elements("LockMultiplatform").ToDictionary(x => x.Attribute("path").Value, x => int.Parse(x.Value)),
            };
        }
        public string GetXML()
        {
            var xdoc = new XDocument();
            var xroot = new XElement("Cache");
            xdoc.Add(xroot);

            foreach (var config in LockMultiplatforms)
            {
                var xlock = new XElement("LockMultiplatform");
                xlock.Add(new XAttribute("path", config.Key));
                xlock.SetValue(config.Value);
                xroot.Add(xlock);
            }

            return xdoc.ToString();
        }
    }
    public sealed class ProjectInfo
    {
        public const string ConfigNotFound = "$$CNFERROR$$";
        public Project Project;
        public bool IsTracing;
        public int Platforms;
        public VerserVersion Version;
        public Config Config;
    }

    public class VerserVersion
    {
        public BigInteger Major;
        public BigInteger Minor;
        public BigInteger Patch;
        public string Suffix;
        public BigInteger? Prerelease;

        public VerserVersion(string s)
        {
            var plusbuild = s.IndexOf('+');
            if (plusbuild != -1) s = s.Substring(0, plusbuild);

            var split = s.Split('.', '-');
            if (split.Length >= 1)
                Major = BigInteger.Parse(split[0]);
            if (split.Length >= 2)
                Minor = BigInteger.Parse(split[1]);
            else Minor = 0;
            if (split.Length >= 3)
                Patch = BigInteger.Parse(split[2]);
            else Patch = 0;
            if (split.Length >= 4)
            {
                var count = split.Length - 3;
                if (count == 1)
                {
                    if (BigInteger.TryParse(split[3], out var prerelease1))
                    {
                        Prerelease = prerelease1;
                    }
                    else
                    {
                        Suffix = split[4];
                    }
                }
                else
                {
                    var minus = 0;
                    if (BigInteger.TryParse(split[split.Length - 1], out var prerelease2))
                    {
                        Prerelease = prerelease2;
                        minus += 1;
                    }
                    Suffix = string.Join(".", split.Skip(3).Take(split.Length - 3 - minus));
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Major);
            sb.Append($".{Minor}");
            sb.Append($".{Patch}");
            if (!string.IsNullOrEmpty(Suffix))
                sb.Append($"-{Suffix}");
            if (Prerelease.HasValue)
                sb.Append($".{Prerelease.Value}");

            return sb.ToString();
        }

        public VerserVersion(VerserVersion verserVersion)
        {
            Major = verserVersion.Major;
            Minor = verserVersion.Minor;
            Patch = verserVersion.Patch;
            Suffix = verserVersion.Suffix;
            Prerelease = verserVersion.Prerelease;
        }

        public VerserVersion UpdateTo(Config config)
        {
            var ret = new VerserVersion(this);

            ret.Suffix = config.Suffix;
            switch (config.Prerelease)
            {
                case Config.PrereleaseType.None:
                    ret.Prerelease = null;
                    break;
                case Config.PrereleaseType.Number:
                case Config.PrereleaseType.Date:
                case Config.PrereleaseType.DateTime:

                    if (config.ResetPrerelease)
                        ret.Prerelease = 0;
                    else
                        ret.Prerelease = Prerelease ?? 0;
                    break;
            }

            return ret;
        }

        public VerserVersion Next(Config config)
        {
            var ret = UpdateTo(config);

            if (config.AppendPrerelease)
            {
                if (!ret.Prerelease.HasValue) ret.Prerelease = 0;
                else ret.Prerelease = Prerelease.Value + 1;
            }
            if (config.AppendPatch)
                ret.Patch += 1;

            return ret;
        }

        public VerserVersion AppendMajor()
        {
            var ret = new VerserVersion(this);

            ret.Major += 1;

            return ret;
        }
        public VerserVersion AppendMinor()
        {
            var ret = new VerserVersion(this);

            ret.Minor += 1;

            return ret;
        }
        public VerserVersion AppendPatch()
        {
            var ret = new VerserVersion(this);

            ret.Patch += 1;

            return ret;
        }
        public VerserVersion AppendPrerelease()
        {
            var ret = new VerserVersion(this);

            if (ret.Prerelease.HasValue)
                ret.Prerelease += 1;

            return ret;
        }
    }
}

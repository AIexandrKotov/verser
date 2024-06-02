using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace verser
{
    public class VerserConfig
    {
        public class Project
        {
            //Saves to verser.config
            public string Path;
            public bool UseVerser;
            public List<Config> Configurations;

            //Saves to verser.cachefile
            public string CurrentConfiguration;
            public Dictionary<string, string> CachedVersions;
        }

        public class Config
        {
            public string ConfigName;
            //Naming:
            public bool UseMinor;
            public bool UseBuild;
            public bool UseRevision;
            public bool UseSuffix;
            public string Suffix;
            public bool UsePostID;
            public PostIDType PostID;

            public bool AppendPostID = true;
            public bool AppendRevision = false;
            public bool AppendBuild = false;
            public bool AppendMinor = false;
            public bool AppendMajor = false;

            public enum PostIDType
            {
                Number,     //0099
                Date,       //20240603
                DateTime,   //202406030013
            }
            //MAJOR[.MINOR][.BUILD][.REVISION][-SUFFIX[.PostID]]
        }
    }
}

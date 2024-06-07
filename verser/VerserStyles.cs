using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace verser
{
    public class VerserStyles
    {
        public Dictionary<string, ConsoleColor> Colors { get; set; }
        public Dictionary<string, (Color, Color)> GUIGolors { get; set; }

        public static VerserStyles GetDefault()
        {
            return new VerserStyles()
            {
                Colors = new Dictionary<string, ConsoleColor>()
                {
                    { "Alpha", ConsoleColor.Red },
                    { "Beta", ConsoleColor.DarkYellow },
                    { "ReleaseCandidate", ConsoleColor.Yellow },
                    { "Release", ConsoleColor.Green },
                },
                GUIGolors = new Dictionary<string, (Color, Color)>()
                {
                    { "Alpha", (Color.FromArgb(192, 0, 0), Color.FromArgb(255, 192, 192)) },
                    { "Beta", (Color.FromArgb(255, 128, 0), Color.FromArgb(255, 224, 192)) },
                    { "ReleaseCandidate", (Color.Olive, Color.FromArgb(255, 255, 192)) },
                    { "Release", (Color.FromArgb(0, 192, 0), Color.FromArgb(192, 255, 192)) },
                },
            };
        }

        public string GetXML()
        {
            var xdoc = new XDocument();
            var xroot = new XElement("VerserStyles");
            xdoc.Add(xroot);

            foreach (var x in Colors)
            {
                var xcolor = new XElement("Color");
                xcolor.Add(new XAttribute("ConfigName", x.Key));
                xcolor.Add(new XAttribute("color", x.Value));
                xroot.Add(xcolor);
            }
            foreach (var x in GUIGolors)
            {
                var xcolor = new XElement("GUIColor");
                xcolor.Add(new XAttribute("ConfigName", x.Key));
                xcolor.Add(new XAttribute("fore", Convert.ToString(x.Value.Item1.ToArgb(), 16)));
                xcolor.Add(new XAttribute("back", Convert.ToString(x.Value.Item2.ToArgb(), 16)));
                xroot.Add(xcolor);
            }

            return xdoc.ToString();
        }
        public static VerserStyles FromXML(string xml)
        {
            var xdoc = XDocument.Parse(xml);
            return new VerserStyles()
            {
                Colors = xdoc.Root.Elements("Color").ToDictionary(
                    x => x.Attribute("ConfigName").Value, 
                    x => (ConsoleColor)Enum.Parse(typeof(ConsoleColor), x.Attribute("color").Value)
                ),
                GUIGolors = xdoc.Root.Elements("GUIColor").ToDictionary(
                    x => x.Attribute("ConfigName").Value,
                    x => (Color.FromArgb(Convert.ToInt32(x.Attribute("fore").Value, 16)), Color.FromArgb(Convert.ToInt32(x.Attribute("back").Value, 16)))
                ),
            };
        }
    }
}

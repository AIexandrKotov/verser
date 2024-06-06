using System;
using System.IO;
using System.Windows.Forms;
using verser;

namespace verser_gui
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            Environment.CurrentDirectory = Path.GetDirectoryName(VerserAPI.VerserExePath);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new VerserMain());
        }
    }
}
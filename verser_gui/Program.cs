using System;
using System.Windows.Forms;

namespace verser_gui
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new VerserMain());
        }
    }
}
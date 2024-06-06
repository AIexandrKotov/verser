using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using verser;

namespace verser_gui
{
    public partial class VerserMain : Form
    {
        public VerserMain()
        {
            InitializeComponent();
            VerserAPI.ReadVerserConfig();
            var projects = verser.Program.GetProjectsByName();
            void AddPanel(ProjectInfo project, bool is_name)
            {
                //var panel = new ProjectPanel();
                //panel.Update(project.Item2[0], true);
                //flowLayoutPanel1.Controls.Add(panel);
            }
            new SuperForm().Show();
            foreach (var project in projects)
            {
                if (project.Item2.Length == 1) AddPanel(project.Item2[0], true);
                else 
                    foreach (var p in project.Item2)
                        AddPanel(p, false);
            }
        }
    }
}

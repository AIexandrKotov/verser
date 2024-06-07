using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using verser;

namespace verser_gui
{
    public partial class VerserMain : Form
    {
        public string CurrentPath;
        private string OldPath = "";

        public VerserMain()
        {
            InitializeComponent();

            VerserVersion.Text = "Verser " + VerserAPI.GetVersionOfAssembly(typeof(VerserAPI).Assembly).ToString();
            VerserGUIVersion.Text = "VerserGUI " + VerserAPI.GetVersionOfAssembly(typeof(VerserMain).Assembly).ToString();
            VerserAPI.Updated += (canceled) =>
            {
                if (canceled) new UpdatedNotification(this).ShowDialog();
                else Close();
            };
            VerserAPI.ReadVerserConfig();
            FullUpdate();
        }

        public Dictionary<string, ProjectPanel> ProjectPanels = new Dictionary<string, ProjectPanel>();
        public void FullUpdate()
        {
            AssignedProjects.Controls.Clear();
            ProjectPanels.Clear();
            var projects = verser.Program.GetProjectsByName();
            var i = 0;
            void AddPanel(ProjectInfo project, bool is_name)
            {
                var panel = new ProjectPanel(this, project, is_name);
                panel.PlaceAs(i++);
                AssignedProjects.Controls.Add(panel);
                ProjectPanels.Add(project.Project.Path, panel);
            }
            foreach (var project in projects)
            {
                if (project.Item2.Length == 1) AddPanel(project.Item2[0], true);
                else
                    foreach (var p in project.Item2)
                        AddPanel(p, false);
            }
            LastAction.Text = $"Projects loaded";
        }
        public void UpdateConfigurationsFor(string path)
        {
            if (CurrentPath == OldPath) return;
            OldPath = CurrentPath;
            CurrentPath = path;

            configurationsListMenu.Items.Clear();
            var project = VerserAPI.GetProjectInfo(path);

            foreach (var c in project.Project.Configurations)
            {
                var config_toolstrip = new ToolStripMenuItem();
                config_toolstrip.BackColor = ProjectPanel.GetBackcolor(c);
                config_toolstrip.Name = "ConfigToolStrip";
                config_toolstrip.Size = new System.Drawing.Size(180, 22);
                config_toolstrip.Text = c.ConfigName;
                config_toolstrip.TextAlign = ContentAlignment.MiddleRight;
                config_toolstrip.Click += (sender, e) =>
                {
                    VerserAPI.SwitchToConfig(path, c.ConfigName);
                    var proj = VerserAPI.GetProjectInfo(path);
                    ProjectPanels[path].UpdateConfiguration(proj);
                    LastAction.Text = $"{proj.Project.Name} config switched to {proj.Config.ConfigName}";
                };
                configurationsListMenu.Items.Add(config_toolstrip);
            }
        }

        public void AppendButton_Click(object sender, EventArgs e, string path)
        {
            VerserAPI.Append(path);
            var project = VerserAPI.GetProjectInfo(path);
            ProjectPanels[path].UpdateVersions(project, project.Config);
            LastAction.Text = $"Version of {project.Project.Name} appended";
        }
        public void TraceButton_Click(object sender, EventArgs e, string path)
        {
            var btn = sender as Button;
            if (btn.Text == "No trace")
                VerserAPI.Trace(path);
            else VerserAPI.Untrace(path);
            var project = VerserAPI.GetProjectInfo(path);
            ProjectPanels[path].UpdateTraceTo(project);
            LastAction.Text = project.IsTracing ? $"Tracing {project.Project.Name}" : $"No tracing {project.Project.Name}";
        }
        public void RemoveButton_Click(object sender, EventArgs e, string path)
        {
            VerserAPI.RemoveProject(path);
            var ind = AssignedProjects.Controls.IndexOf(ProjectPanels[path]);
            AssignedProjects.Controls.RemoveAt(ind);
            for (var i = ind; i < AssignedProjects.Controls.Count; i++)
                (AssignedProjects.Controls[i] as ProjectPanel).PlaceAs(i);
            LastAction.Text = $"Removed project {path}";
        }
        public void Configuration_Click(object sender, EventArgs e, string path)
        {
            UpdateConfigurationsFor(path);
            Button btnSender = (Button)sender;
            if (configurationsListMenu.Visible)
                configurationsListMenu.Hide();
            else
            {
                configurationsListMenu.Show(btnSender, new Point(btnSender.Width - configurationsListMenu.Width, btnSender.Height));
            }
        }
        public void Configurations_Click(object sender, EventArgs e, string path)
        {

        }
        public void Appends_Click(object sender, EventArgs e, string path)
        {
            CurrentPath = path;
            Button btnSender = (Button)sender;
            if (appendsMenu.Visible)
                appendsMenu.Hide();
            else
            {
                appendsMenu.Show(btnSender, new Point(btnSender.Width - appendsMenu.Width, btnSender.Height));
            }
        }

        private void AssignProjectButton_Click(object sender, EventArgs e)
        {
            var open = new OpenFileDialog();
            open.Filter = "CSharp project|*.csproj";
            if (open.ShowDialog() == DialogResult.OK)
            {
                VerserAPI.AddProject(open.FileName);
                var project = VerserAPI.GetProjectInfo(open.FileName);
                var panel = new ProjectPanel(this, project, false);
                panel.PlaceAs(AssignedProjects.Controls.Count);
                AssignedProjects.Controls.Add(panel);
                ProjectPanels.Add(project.Project.Path, panel);
            }
        }
        private void MajorAppendButton_Click(object sender, EventArgs e)
        {
            VerserAPI.Major(CurrentPath);
            var project = VerserAPI.GetProjectInfo(CurrentPath);
            ProjectPanels[CurrentPath].UpdateVersions(project, project.Config);
            LastAction.Text = $"Major version of {project.Project.Name} appended";
        }
        private void MinorAppendButton_Click(object sender, EventArgs e)
        {
            VerserAPI.Minor(CurrentPath);
            var project = VerserAPI.GetProjectInfo(CurrentPath);
            ProjectPanels[CurrentPath].UpdateVersions(project, project.Config);
            LastAction.Text = $"Minor version of {project.Project.Name} appended";
        }
        private void PatchAppendButton_Click(object sender, EventArgs e)
        {
            VerserAPI.Patch(CurrentPath);
            var project = VerserAPI.GetProjectInfo(CurrentPath);
            ProjectPanels[CurrentPath].UpdateVersions(project, project.Config);
            LastAction.Text = $"Patch version of {project.Project.Name} appended";
        }
    }
}

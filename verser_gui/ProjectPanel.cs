using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using verser;

namespace verser_gui
{
    public class ProjectPanel : Panel
    {
        public readonly VerserMain Main;
        public readonly string ProjectPath;

        public static Color GetForecolor(Config config)
        {
            if (config != null && VerserAPI.Style.GUIGolors.TryGetValue(config.ConfigName, out var value))
                return value.Item1;
            return Color.Black;
        }
        public static Color GetBackcolor(Config config)
        {
            if (config != null && VerserAPI.Style.GUIGolors.TryGetValue(config.ConfigName, out var value))
                return value.Item2;
            return Color.White;
        }

        public ProjectPanel(VerserMain main, ProjectInfo project, bool is_name)
        {
            Main = main;
            ProjectPath = project.Project.Path;
            this.ConfigurationsButton = new System.Windows.Forms.Button();
            this.PlatformsCount = new System.Windows.Forms.Label();
            this.AppendsButton = new System.Windows.Forms.Button();
            this.projectCheck = new System.Windows.Forms.CheckBox();
            this.TraceButton = new System.Windows.Forms.Button();
            this.ConfigButton = new System.Windows.Forms.Button();
            this.AppendButton = new System.Windows.Forms.Button();
            this.NextVersion = new System.Windows.Forms.Label();
            this.CurrentVersion = new System.Windows.Forms.Label();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.ProjectName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // projectPanel
            // 
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.ConfigurationsButton);
            this.Controls.Add(this.PlatformsCount);
            this.Controls.Add(this.AppendsButton);
            this.Controls.Add(this.projectCheck);
            this.Controls.Add(this.TraceButton);
            this.Controls.Add(this.ConfigButton);
            this.Controls.Add(this.AppendButton);
            this.Controls.Add(this.NextVersion);
            this.Controls.Add(this.CurrentVersion);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.ProjectName);
            this.Location = new System.Drawing.Point(12, 12);
            this.Name = "projectPanel";
            this.Size = new System.Drawing.Size(740, 55);
            this.TabIndex = 0;
            // 
            // ConfigurationsButton
            // 
            this.ConfigurationsButton.BackColor = System.Drawing.Color.White;
            this.ConfigurationsButton.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ConfigurationsButton.Location = new System.Drawing.Point(468, 27);
            this.ConfigurationsButton.Name = "ConfigurationsButton";
            this.ConfigurationsButton.Size = new System.Drawing.Size(178, 23);
            this.ConfigurationsButton.TabIndex = 9;
            this.ConfigurationsButton.Text = "Configurations";
            this.ConfigurationsButton.UseVisualStyleBackColor = true;
            this.ConfigurationsButton.Click += (sender, e) => Main.Configurations_Click(sender, e, ProjectPath);
            // 
            // PlatformsCount
            // 
            this.PlatformsCount.Location = new System.Drawing.Point(391, 27);
            this.PlatformsCount.Name = "PlatformsCount";
            this.PlatformsCount.Size = new System.Drawing.Size(71, 23);
            this.PlatformsCount.TabIndex = 8;
            this.PlatformsCount.Text = $"Platforms: {project.Platforms}";
            this.PlatformsCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AppendsButton
            // 
            this.AppendsButton.BackColor = System.Drawing.Color.White;
            this.AppendsButton.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AppendsButton.Location = new System.Drawing.Point(652, 27);
            this.AppendsButton.Name = "AppendsButton";
            this.AppendsButton.Size = new System.Drawing.Size(83, 23);
            this.AppendsButton.TabIndex = 10;
            this.AppendsButton.Text = "Append ⯆";
            this.AppendsButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AppendsButton.UseVisualStyleBackColor = true;
            this.AppendsButton.Click += (sender, e) => Main.Appends_Click(sender, e, ProjectPath);
            //this.AppendsButton.Click += new System.EventHandler(this.button26_Click);
            // 
            // projectCheck
            // 
            this.projectCheck.AutoSize = true;
            this.projectCheck.Location = new System.Drawing.Point(7, 7);
            this.projectCheck.Name = "projectCheck";
            this.projectCheck.Size = new System.Drawing.Size(15, 14);
            this.projectCheck.TabIndex = 0;
            this.projectCheck.UseVisualStyleBackColor = true;
            // 
            // TraceButton
            // 
            this.TraceButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.TraceButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.TraceButton.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TraceButton.ForeColor = System.Drawing.Color.Black;
            this.TraceButton.Location = new System.Drawing.Point(6, 27);
            this.TraceButton.Name = "TraceButton";
            this.TraceButton.Size = new System.Drawing.Size(100, 23);
            this.TraceButton.TabIndex = 6;
            this.TraceButton.UseVisualStyleBackColor = false;
            this.TraceButton.Click += (sender, e) => Main.TraceButton_Click(sender, e, ProjectPath);
            UpdateTraceTo(project);
            // 
            // ConfigButton
            // 
            this.ConfigButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ConfigButton.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ConfigButton.Location = new System.Drawing.Point(112, 27);
            this.ConfigButton.Name = "ConfigButton";
            this.ConfigButton.Size = new System.Drawing.Size(273, 23);
            this.ConfigButton.TabIndex = 7;
            this.ConfigButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ConfigButton.UseVisualStyleBackColor = false;
            this.ConfigButton.Click += (sender, e) => Main.Configuration_Click(sender, e, ProjectPath);
            UpdateConfiguration(project);
            //this.ConfigButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // AppendButton
            // 
            this.AppendButton.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AppendButton.Location = new System.Drawing.Point(500, 3);
            this.AppendButton.Name = "AppendButton";
            this.AppendButton.Size = new System.Drawing.Size(34, 23);
            this.AppendButton.TabIndex = 3;
            this.AppendButton.Text = "->";
            this.AppendButton.UseVisualStyleBackColor = true;
            this.AppendButton.Click += (sender, e) => Main.AppendButton_Click(sender, e, ProjectPath);
            // 
            // NextVersion
            // 
            this.NextVersion.BackColor = System.Drawing.Color.Transparent;
            this.NextVersion.ForeColor = GetForecolor(project.Config);
            this.NextVersion.Location = new System.Drawing.Point(540, 3);
            this.NextVersion.Name = "NextVersion";
            this.NextVersion.Size = new System.Drawing.Size(135, 23);
            this.NextVersion.TabIndex = 4;
            this.NextVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CurrentVersion
            // 
            this.CurrentVersion.BackColor = System.Drawing.Color.Transparent;
            this.CurrentVersion.Location = new System.Drawing.Point(359, 3);
            this.CurrentVersion.Name = "CurrentVersion";
            this.CurrentVersion.Size = new System.Drawing.Size(135, 23);
            this.CurrentVersion.TabIndex = 2;
            this.CurrentVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RemoveButton
            // 
            this.RemoveButton.BackColor = System.Drawing.Color.White;
            this.RemoveButton.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RemoveButton.Location = new System.Drawing.Point(712, 3);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(23, 23);
            this.RemoveButton.TabIndex = 5;
            this.RemoveButton.Text = "X";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += (sender, e) => Main.RemoveButton_Click(sender, e, ProjectPath);
            // 
            // ProjectName
            // 
            this.ProjectName.Location = new System.Drawing.Point(28, 2);
            this.ProjectName.Name = "ProjectName";
            this.ProjectName.Size = new System.Drawing.Size(325, 23);
            this.ProjectName.TabIndex = 1;
            this.ProjectName.Text = is_name ? project.Project.Name : $"{project.Project.Name} at {Path.GetDirectoryName(project.Project.Path)}";
            this.ProjectName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            ResumeLayout(false);
            PerformLayout();
        }

        public void UpdateConfiguration(ProjectInfo project)
        {
            this.ConfigButton.BackColor = GetBackcolor(project.Config);
            this.ConfigButton.Text = $"{project.Config?.ConfigName ?? ""} ⯆";
            UpdateVersions(project, project.Config);
        }

        public void UpdateVersions(ProjectInfo project, Config config)
        {
            this.CurrentVersion.Text = project.Version.ToString();
            this.NextVersion.Text = project.IsTracing && config != null ? project.Version.Next(config).ToString() : project.Version.ToString();
        }
        public void UpdateTraceTo(ProjectInfo project)
        {
            if (project.IsTracing)
            {
                TraceButton.Text = "Trace";
                TraceButton.BackColor = Color.FromArgb(192, 255, 192);
            }
            else
            {
                TraceButton.Text = "No trace";
                TraceButton.BackColor = Color.FromArgb(255, 192, 192);
            }
            UpdateVersions(project, project.Config);
        }
        
        public void PlaceAs(int index)
        {
            Location = new Point(22, 12 + 60 * index);
        }

        private System.Windows.Forms.CheckBox projectCheck;
        private System.Windows.Forms.Label ProjectName;
        private System.Windows.Forms.Label CurrentVersion;
        private System.Windows.Forms.Button ConfigButton;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.Button AppendButton;
        private System.Windows.Forms.Label NextVersion;
        private System.Windows.Forms.Button TraceButton;
        private System.Windows.Forms.Button AppendsButton;
        private System.Windows.Forms.Label PlatformsCount;
        private System.Windows.Forms.Button ConfigurationsButton;
    }
}

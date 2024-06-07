namespace verser_gui
{
    partial class SuperForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.projectPanel = new System.Windows.Forms.Panel();
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
            this.appendsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.majorAppendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minorAppendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patchAppendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationsListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.alphaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.betaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.releaseCandidateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.releaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectPanel.SuspendLayout();
            this.appendsMenu.SuspendLayout();
            this.configurationsListMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // projectPanel
            // 
            this.projectPanel.BackColor = System.Drawing.Color.White;
            this.projectPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.projectPanel.Controls.Add(this.ConfigurationsButton);
            this.projectPanel.Controls.Add(this.PlatformsCount);
            this.projectPanel.Controls.Add(this.AppendsButton);
            this.projectPanel.Controls.Add(this.projectCheck);
            this.projectPanel.Controls.Add(this.TraceButton);
            this.projectPanel.Controls.Add(this.ConfigButton);
            this.projectPanel.Controls.Add(this.AppendButton);
            this.projectPanel.Controls.Add(this.NextVersion);
            this.projectPanel.Controls.Add(this.CurrentVersion);
            this.projectPanel.Controls.Add(this.RemoveButton);
            this.projectPanel.Controls.Add(this.ProjectName);
            this.projectPanel.Location = new System.Drawing.Point(12, 12);
            this.projectPanel.Name = "projectPanel";
            this.projectPanel.Size = new System.Drawing.Size(740, 55);
            this.projectPanel.TabIndex = 0;
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
            // 
            // PlatformsCount
            // 
            this.PlatformsCount.Location = new System.Drawing.Point(391, 27);
            this.PlatformsCount.Name = "PlatformsCount";
            this.PlatformsCount.Size = new System.Drawing.Size(71, 23);
            this.PlatformsCount.TabIndex = 8;
            this.PlatformsCount.Text = "Platforms: 99";
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
            this.AppendsButton.Click += new System.EventHandler(this.button26_Click);
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
            this.TraceButton.Text = "Traced";
            this.TraceButton.UseVisualStyleBackColor = false;
            // 
            // ConfigButton
            // 
            this.ConfigButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ConfigButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ConfigButton.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ConfigButton.Location = new System.Drawing.Point(112, 27);
            this.ConfigButton.Name = "ConfigButton";
            this.ConfigButton.Size = new System.Drawing.Size(273, 23);
            this.ConfigButton.TabIndex = 7;
            this.ConfigButton.Text = "Release ⯆";
            this.ConfigButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ConfigButton.UseVisualStyleBackColor = false;
            this.ConfigButton.Click += new System.EventHandler(this.button1_Click);
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
            // 
            // NextVersion
            // 
            this.NextVersion.BackColor = System.Drawing.Color.Transparent;
            this.NextVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.NextVersion.Location = new System.Drawing.Point(540, 3);
            this.NextVersion.Name = "NextVersion";
            this.NextVersion.Size = new System.Drawing.Size(135, 23);
            this.NextVersion.TabIndex = 4;
            this.NextVersion.Text = "0.1.1";
            this.NextVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CurrentVersion
            // 
            this.CurrentVersion.BackColor = System.Drawing.Color.Transparent;
            this.CurrentVersion.Location = new System.Drawing.Point(359, 3);
            this.CurrentVersion.Name = "CurrentVersion";
            this.CurrentVersion.Size = new System.Drawing.Size(135, 23);
            this.CurrentVersion.TabIndex = 2;
            this.CurrentVersion.Text = "0.1.0";
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
            // 
            // ProjectName
            // 
            this.ProjectName.Location = new System.Drawing.Point(28, 2);
            this.ProjectName.Name = "ProjectName";
            this.ProjectName.Size = new System.Drawing.Size(325, 23);
            this.ProjectName.TabIndex = 1;
            this.ProjectName.Text = "projectName or PATH";
            this.ProjectName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // appendsMenu
            // 
            this.appendsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.majorAppendToolStripMenuItem,
            this.minorAppendToolStripMenuItem,
            this.patchAppendToolStripMenuItem});
            this.appendsMenu.Name = "appendsMenu";
            this.appendsMenu.Size = new System.Drawing.Size(150, 70);
            // 
            // majorAppendToolStripMenuItem
            // 
            this.majorAppendToolStripMenuItem.Name = "majorAppendToolStripMenuItem";
            this.majorAppendToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.majorAppendToolStripMenuItem.Text = "Major append";
            // 
            // minorAppendToolStripMenuItem
            // 
            this.minorAppendToolStripMenuItem.Name = "minorAppendToolStripMenuItem";
            this.minorAppendToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.minorAppendToolStripMenuItem.Text = "Minor append";
            // 
            // patchAppendToolStripMenuItem
            // 
            this.patchAppendToolStripMenuItem.Name = "patchAppendToolStripMenuItem";
            this.patchAppendToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.patchAppendToolStripMenuItem.Text = "Patch append";
            // 
            // configurationsListMenu
            // 
            this.configurationsListMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alphaToolStripMenuItem,
            this.betaToolStripMenuItem,
            this.releaseCandidateToolStripMenuItem,
            this.releaseToolStripMenuItem});
            this.configurationsListMenu.Name = "configurationsListMenu";
            this.configurationsListMenu.Size = new System.Drawing.Size(181, 114);
            // 
            // alphaToolStripMenuItem
            // 
            this.alphaToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.alphaToolStripMenuItem.Name = "alphaToolStripMenuItem";
            this.alphaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.alphaToolStripMenuItem.Text = "Alpha";
            this.alphaToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // betaToolStripMenuItem
            // 
            this.betaToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.betaToolStripMenuItem.Name = "betaToolStripMenuItem";
            this.betaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.betaToolStripMenuItem.Text = "Beta";
            this.betaToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // releaseCandidateToolStripMenuItem
            // 
            this.releaseCandidateToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.releaseCandidateToolStripMenuItem.Name = "releaseCandidateToolStripMenuItem";
            this.releaseCandidateToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.releaseCandidateToolStripMenuItem.Text = "ReleaseCandidate";
            this.releaseCandidateToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // releaseToolStripMenuItem
            // 
            this.releaseToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.releaseToolStripMenuItem.Name = "releaseToolStripMenuItem";
            this.releaseToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.releaseToolStripMenuItem.Text = "Release";
            this.releaseToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SuperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 451);
            this.Controls.Add(this.projectPanel);
            this.Name = "SuperForm";
            this.Text = "SuperForm";
            this.projectPanel.ResumeLayout(false);
            this.projectPanel.PerformLayout();
            this.appendsMenu.ResumeLayout(false);
            this.configurationsListMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel projectPanel;
        private System.Windows.Forms.Label ProjectName;
        private System.Windows.Forms.Button ConfigButton;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.Label CurrentVersion;
        private System.Windows.Forms.Button AppendButton;
        private System.Windows.Forms.Label NextVersion;
        private System.Windows.Forms.Button TraceButton;
        private System.Windows.Forms.CheckBox projectCheck;
        private System.Windows.Forms.Button AppendsButton;
        private System.Windows.Forms.ContextMenuStrip appendsMenu;

        private System.Windows.Forms.ToolStripMenuItem majorAppendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minorAppendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patchAppendToolStripMenuItem;

        private System.Windows.Forms.ContextMenuStrip configurationsListMenu;

        private System.Windows.Forms.ToolStripMenuItem alphaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem betaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem releaseCandidateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem releaseToolStripMenuItem;

        private System.Windows.Forms.Label PlatformsCount;
        private System.Windows.Forms.Button ConfigurationsButton;
    }
}
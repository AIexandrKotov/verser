namespace verser_gui
{
    partial class VerserMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VerserMain));
            this.AssignedProjects = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.AssignProjectButton = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.LastAction = new System.Windows.Forms.ToolStripStatusLabel();
            this.VerserVersions = new System.Windows.Forms.ToolStripDropDownButton();
            this.VerserGUIVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.VerserVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.appendsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MajorAppendButton = new System.Windows.Forms.ToolStripMenuItem();
            this.MinorAppendButton = new System.Windows.Forms.ToolStripMenuItem();
            this.PatchAppendButton = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationsListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.appendsMenu.SuspendLayout();
            this.configurationsListMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // AssignedProjects
            // 
            this.AssignedProjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AssignedProjects.AutoScroll = true;
            this.AssignedProjects.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.AssignedProjects.Location = new System.Drawing.Point(12, 27);
            this.AssignedProjects.Name = "AssignedProjects";
            this.AssignedProjects.Size = new System.Drawing.Size(764, 309);
            this.AssignedProjects.TabIndex = 0;
            this.AssignedProjects.WrapContents = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AssignProjectButton});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // AssignProjectButton
            // 
            this.AssignProjectButton.Name = "AssignProjectButton";
            this.AssignProjectButton.Size = new System.Drawing.Size(94, 20);
            this.AssignProjectButton.Text = "Assign project";
            this.AssignProjectButton.Click += new System.EventHandler(this.AssignProjectButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LastAction,
            this.VerserVersions,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 339);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // LastAction
            // 
            this.LastAction.Name = "LastAction";
            this.LastAction.Size = new System.Drawing.Size(593, 17);
            this.LastAction.Spring = true;
            this.LastAction.Text = "Ready";
            // 
            // VerserVersions
            // 
            this.VerserVersions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.VerserVersions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.VerserGUIVersion,
            this.VerserVersion});
            this.VerserVersions.Image = ((System.Drawing.Image)(resources.GetObject("VerserVersions.Image")));
            this.VerserVersions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.VerserVersions.Name = "VerserVersions";
            this.VerserVersions.ShowDropDownArrow = false;
            this.VerserVersions.Size = new System.Drawing.Size(42, 20);
            this.VerserVersions.Text = "Verser";
            // 
            // VerserGUIVersion
            // 
            this.VerserGUIVersion.Name = "VerserGUIVersion";
            this.VerserGUIVersion.Size = new System.Drawing.Size(151, 22);
            this.VerserGUIVersion.Text = "VerserGUI 0.1.0";
            // 
            // VerserVersion
            // 
            this.VerserVersion.Name = "VerserVersion";
            this.VerserVersion.Size = new System.Drawing.Size(151, 22);
            this.VerserVersion.Text = "Verser 0.1.0";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(134, 17);
            this.toolStripStatusLabel2.Text = "made by AIexandrKotov";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // appendsMenu
            // 
            this.appendsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MajorAppendButton,
            this.MinorAppendButton,
            this.PatchAppendButton});
            this.appendsMenu.Name = "appendsMenu";
            this.appendsMenu.Size = new System.Drawing.Size(150, 70);
            // 
            // MajorAppendButton
            // 
            this.MajorAppendButton.Name = "MajorAppendButton";
            this.MajorAppendButton.Size = new System.Drawing.Size(149, 22);
            this.MajorAppendButton.Text = "Major append";
            this.MajorAppendButton.Click += new System.EventHandler(this.MajorAppendButton_Click);
            // 
            // MinorAppendButton
            // 
            this.MinorAppendButton.Name = "MinorAppendButton";
            this.MinorAppendButton.Size = new System.Drawing.Size(149, 22);
            this.MinorAppendButton.Text = "Minor append";
            this.MinorAppendButton.Click += new System.EventHandler(this.MinorAppendButton_Click);
            // 
            // PatchAppendButton
            // 
            this.PatchAppendButton.Name = "PatchAppendButton";
            this.PatchAppendButton.Size = new System.Drawing.Size(149, 22);
            this.PatchAppendButton.Text = "Patch append";
            this.PatchAppendButton.Click += new System.EventHandler(this.PatchAppendButton_Click);
            // 
            // configurationsListMenu
            // 
            this.configurationsListMenu.Name = "configurationsListMenu";
            this.configurationsListMenu.Size = new System.Drawing.Size(181, 114);
            // 
            // VerserMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 361);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.AssignedProjects);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 9999);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 200);
            this.Name = "VerserMain";
            this.ShowIcon = false;
            this.Text = "Verser";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.appendsMenu.ResumeLayout(false);
            this.configurationsListMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel AssignedProjects;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem AssignProjectButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripStatusLabel LastAction;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripDropDownButton VerserVersions;
        private System.Windows.Forms.ToolStripMenuItem VerserGUIVersion;
        private System.Windows.Forms.ToolStripMenuItem VerserVersion;
        private System.Windows.Forms.ContextMenuStrip appendsMenu;
        private System.Windows.Forms.ToolStripMenuItem MajorAppendButton;
        private System.Windows.Forms.ToolStripMenuItem MinorAppendButton;
        private System.Windows.Forms.ToolStripMenuItem PatchAppendButton;
        private System.Windows.Forms.ContextMenuStrip configurationsListMenu;
    }
}
namespace krassequenzer
{
    partial class Form1
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemNewComposition = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
			this.compositionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemAddTrack = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemCompositionProperties = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonUpdate = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonDebugBreak = new System.Windows.Forms.ToolStripButton();
			this.panelTrackOverviewContainer = new System.Windows.Forms.Panel();
			this.toolStripMenuItemHardware = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDeviceSetup = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.compositionToolStripMenuItem,
            this.toolStripMenuItemHardware});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(857, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemNewComposition,
            this.toolStripMenuItemExit});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// toolStripMenuItemNewComposition
			// 
			this.toolStripMenuItemNewComposition.Name = "toolStripMenuItemNewComposition";
			this.toolStripMenuItemNewComposition.Size = new System.Drawing.Size(170, 22);
			this.toolStripMenuItemNewComposition.Text = "&New Composition";
			this.toolStripMenuItemNewComposition.Click += new System.EventHandler(this.toolStripMenuItemNewComposition_Click);
			// 
			// toolStripMenuItemExit
			// 
			this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
			this.toolStripMenuItemExit.Size = new System.Drawing.Size(170, 22);
			this.toolStripMenuItemExit.Text = "E&xit";
			this.toolStripMenuItemExit.Click += new System.EventHandler(this.toolStripMenuItemExit_Click);
			// 
			// compositionToolStripMenuItem
			// 
			this.compositionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAddTrack,
            this.toolStripMenuItemCompositionProperties});
			this.compositionToolStripMenuItem.Name = "compositionToolStripMenuItem";
			this.compositionToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
			this.compositionToolStripMenuItem.Text = "&Composition";
			// 
			// toolStripMenuItemAddTrack
			// 
			this.toolStripMenuItemAddTrack.Name = "toolStripMenuItemAddTrack";
			this.toolStripMenuItemAddTrack.Size = new System.Drawing.Size(152, 22);
			this.toolStripMenuItemAddTrack.Text = "Add &Track";
			this.toolStripMenuItemAddTrack.Click += new System.EventHandler(this.toolStripMenuItemAddTrack_Click);
			// 
			// toolStripMenuItemCompositionProperties
			// 
			this.toolStripMenuItemCompositionProperties.Name = "toolStripMenuItemCompositionProperties";
			this.toolStripMenuItemCompositionProperties.Size = new System.Drawing.Size(152, 22);
			this.toolStripMenuItemCompositionProperties.Text = "&Properties...";
			this.toolStripMenuItemCompositionProperties.Click += new System.EventHandler(this.toolStripMenuItemCompositionProperties_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelStatus});
			this.statusStrip1.Location = new System.Drawing.Point(0, 419);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(857, 22);
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabelStatus
			// 
			this.toolStripStatusLabelStatus.Name = "toolStripStatusLabelStatus";
			this.toolStripStatusLabelStatus.Size = new System.Drawing.Size(61, 17);
			this.toolStripStatusLabelStatus.Text = "Initializing";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonUpdate,
            this.toolStripButtonDebugBreak});
			this.toolStrip1.Location = new System.Drawing.Point(0, 24);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(857, 25);
			this.toolStrip1.TabIndex = 2;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButtonUpdate
			// 
			this.toolStripButtonUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonUpdate.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUpdate.Image")));
			this.toolStripButtonUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonUpdate.Name = "toolStripButtonUpdate";
			this.toolStripButtonUpdate.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonUpdate.Text = "Update";
			this.toolStripButtonUpdate.Click += new System.EventHandler(this.toolStripButtonUpdate_Click);
			// 
			// toolStripButtonDebugBreak
			// 
			this.toolStripButtonDebugBreak.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonDebugBreak.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDebugBreak.Image")));
			this.toolStripButtonDebugBreak.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonDebugBreak.Name = "toolStripButtonDebugBreak";
			this.toolStripButtonDebugBreak.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonDebugBreak.Text = "DEBUG BREAK";
			this.toolStripButtonDebugBreak.Click += new System.EventHandler(this.toolStripButtonDebugBreak_Click);
			// 
			// panelTrackOverviewContainer
			// 
			this.panelTrackOverviewContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelTrackOverviewContainer.Location = new System.Drawing.Point(12, 52);
			this.panelTrackOverviewContainer.Name = "panelTrackOverviewContainer";
			this.panelTrackOverviewContainer.Size = new System.Drawing.Size(833, 364);
			this.panelTrackOverviewContainer.TabIndex = 3;
			// 
			// toolStripMenuItemHardware
			// 
			this.toolStripMenuItemHardware.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDeviceSetup});
			this.toolStripMenuItemHardware.Name = "toolStripMenuItemHardware";
			this.toolStripMenuItemHardware.Size = new System.Drawing.Size(70, 20);
			this.toolStripMenuItemHardware.Text = "&Hardware";
			// 
			// toolStripMenuItemDeviceSetup
			// 
			this.toolStripMenuItemDeviceSetup.Name = "toolStripMenuItemDeviceSetup";
			this.toolStripMenuItemDeviceSetup.Size = new System.Drawing.Size(152, 22);
			this.toolStripMenuItemDeviceSetup.Text = "&Device Setup...";
			this.toolStripMenuItemDeviceSetup.Click += new System.EventHandler(this.toolStripMenuItemDeviceSetup_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(857, 441);
			this.Controls.Add(this.panelTrackOverviewContainer);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "Form1";
			this.Text = "Form1";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButtonUpdate;
		private System.Windows.Forms.ToolStripMenuItem compositionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCompositionProperties;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNewComposition;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatus;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;
		private System.Windows.Forms.Panel panelTrackOverviewContainer;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAddTrack;
		private System.Windows.Forms.ToolStripButton toolStripButtonDebugBreak;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHardware;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeviceSetup;
    }
}


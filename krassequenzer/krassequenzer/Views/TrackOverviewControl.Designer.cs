namespace krassequenzer.Views
{
	partial class TrackOverviewControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.labelTimelinePlaceholder = new System.Windows.Forms.Label();
			this.panelTrackContentContainer = new System.Windows.Forms.Panel();
			this.panelTrackHeaderContainer = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// labelTimelinePlaceholder
			// 
			this.labelTimelinePlaceholder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelTimelinePlaceholder.BackColor = System.Drawing.Color.Black;
			this.labelTimelinePlaceholder.ForeColor = System.Drawing.Color.White;
			this.labelTimelinePlaceholder.Location = new System.Drawing.Point(200, 0);
			this.labelTimelinePlaceholder.Margin = new System.Windows.Forms.Padding(0);
			this.labelTimelinePlaceholder.Name = "labelTimelinePlaceholder";
			this.labelTimelinePlaceholder.Size = new System.Drawing.Size(511, 23);
			this.labelTimelinePlaceholder.TabIndex = 0;
			this.labelTimelinePlaceholder.Text = "LOL TIMELINE";
			// 
			// panelTrackContentContainer
			// 
			this.panelTrackContentContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelTrackContentContainer.Location = new System.Drawing.Point(200, 23);
			this.panelTrackContentContainer.Margin = new System.Windows.Forms.Padding(0);
			this.panelTrackContentContainer.Name = "panelTrackContentContainer";
			this.panelTrackContentContainer.Size = new System.Drawing.Size(511, 285);
			this.panelTrackContentContainer.TabIndex = 1;
			// 
			// panelTrackHeaderContainer
			// 
			this.panelTrackHeaderContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.panelTrackHeaderContainer.Location = new System.Drawing.Point(0, 23);
			this.panelTrackHeaderContainer.Margin = new System.Windows.Forms.Padding(0);
			this.panelTrackHeaderContainer.Name = "panelTrackHeaderContainer";
			this.panelTrackHeaderContainer.Size = new System.Drawing.Size(200, 285);
			this.panelTrackHeaderContainer.TabIndex = 1;
			// 
			// TrackOverviewControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.Controls.Add(this.panelTrackHeaderContainer);
			this.Controls.Add(this.panelTrackContentContainer);
			this.Controls.Add(this.labelTimelinePlaceholder);
			this.Name = "TrackOverviewControl";
			this.Size = new System.Drawing.Size(711, 308);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label labelTimelinePlaceholder;
		private System.Windows.Forms.Panel panelTrackContentContainer;
		private System.Windows.Forms.Panel panelTrackHeaderContainer;
	}
}

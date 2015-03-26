namespace krassequenzer.Views
{
	partial class TrackHeaderControl
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
			this.textBoxTrackName = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// textBoxTrackName
			// 
			this.textBoxTrackName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxTrackName.Location = new System.Drawing.Point(3, 3);
			this.textBoxTrackName.Name = "textBoxTrackName";
			this.textBoxTrackName.Size = new System.Drawing.Size(194, 20);
			this.textBoxTrackName.TabIndex = 0;
			this.textBoxTrackName.TextChanged += new System.EventHandler(this.textBoxTrackName_TextChanged);
			// 
			// TrackHeaderControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.Controls.Add(this.textBoxTrackName);
			this.Name = "TrackHeaderControl";
			this.Size = new System.Drawing.Size(200, 150);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxTrackName;
	}
}

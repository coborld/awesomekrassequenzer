namespace krassequenzer.Views
{
	partial class TrackContentControl
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
			this.labelContentCat = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// labelContentCat
			// 
			this.labelContentCat.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelContentCat.Location = new System.Drawing.Point(0, 0);
			this.labelContentCat.Name = "labelContentCat";
			this.labelContentCat.Size = new System.Drawing.Size(150, 150);
			this.labelContentCat.TabIndex = 0;
			this.labelContentCat.Text = "label1";
			// 
			// TrackContentControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.DarkGoldenrod;
			this.Controls.Add(this.labelContentCat);
			this.Name = "TrackContentControl";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label labelContentCat;
	}
}

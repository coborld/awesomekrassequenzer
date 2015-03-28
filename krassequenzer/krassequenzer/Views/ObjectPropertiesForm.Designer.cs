namespace krassequenzer.Views
{
	partial class ObjectPropertiesForm
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
			this.panelControlContainer = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// panelControlContainer
			// 
			this.panelControlContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelControlContainer.Location = new System.Drawing.Point(12, 12);
			this.panelControlContainer.Name = "panelControlContainer";
			this.panelControlContainer.Size = new System.Drawing.Size(260, 238);
			this.panelControlContainer.TabIndex = 0;
			// 
			// ObjectPropertiesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.panelControlContainer);
			this.Name = "ObjectPropertiesForm";
			this.Text = "ObjectPropertiesForm";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelControlContainer;
	}
}
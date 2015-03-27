namespace krassequenzer.Views
{
	partial class DeviceSetupForm
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
			this.listViewMidiOutputInterfaces = new System.Windows.Forms.ListView();
			this.labelMidiOutputInterfaceListHint = new System.Windows.Forms.Label();
			this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderDeviceID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.SuspendLayout();
			// 
			// listViewMidiOutputInterfaces
			// 
			this.listViewMidiOutputInterfaces.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listViewMidiOutputInterfaces.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderDeviceID});
			this.listViewMidiOutputInterfaces.FullRowSelect = true;
			this.listViewMidiOutputInterfaces.GridLines = true;
			this.listViewMidiOutputInterfaces.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listViewMidiOutputInterfaces.Location = new System.Drawing.Point(12, 25);
			this.listViewMidiOutputInterfaces.Name = "listViewMidiOutputInterfaces";
			this.listViewMidiOutputInterfaces.Size = new System.Drawing.Size(260, 225);
			this.listViewMidiOutputInterfaces.TabIndex = 0;
			this.listViewMidiOutputInterfaces.UseCompatibleStateImageBehavior = false;
			this.listViewMidiOutputInterfaces.View = System.Windows.Forms.View.Details;
			this.listViewMidiOutputInterfaces.SelectedIndexChanged += new System.EventHandler(this.listViewMidiOutputInterfaces_SelectedIndexChanged);
			// 
			// labelMidiOutputInterfaceListHint
			// 
			this.labelMidiOutputInterfaceListHint.AutoSize = true;
			this.labelMidiOutputInterfaceListHint.Location = new System.Drawing.Point(12, 9);
			this.labelMidiOutputInterfaceListHint.Name = "labelMidiOutputInterfaceListHint";
			this.labelMidiOutputInterfaceListHint.Size = new System.Drawing.Size(110, 13);
			this.labelMidiOutputInterfaceListHint.TabIndex = 1;
			this.labelMidiOutputInterfaceListHint.Text = "MIDI Output Interface";
			// 
			// columnHeaderName
			// 
			this.columnHeaderName.Text = "Name";
			this.columnHeaderName.Width = 174;
			// 
			// columnHeaderDeviceID
			// 
			this.columnHeaderDeviceID.Text = "Device ID";
			// 
			// DeviceSetupForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.labelMidiOutputInterfaceListHint);
			this.Controls.Add(this.listViewMidiOutputInterfaces);
			this.Name = "DeviceSetupForm";
			this.Text = "Device Setup";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView listViewMidiOutputInterfaces;
		private System.Windows.Forms.Label labelMidiOutputInterfaceListHint;
		private System.Windows.Forms.ColumnHeader columnHeaderName;
		private System.Windows.Forms.ColumnHeader columnHeaderDeviceID;
	}
}
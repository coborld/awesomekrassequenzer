namespace krassequenzer.Views
{
	partial class ListEditorControl
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
			this.comboBoxEventDisplay = new System.Windows.Forms.ComboBox();
			this.labelEventDisplayHint = new System.Windows.Forms.Label();
			this.listViewEventList = new System.Windows.Forms.ListView();
			this.columnHeaderStartPosition = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.SuspendLayout();
			// 
			// comboBoxEventDisplay
			// 
			this.comboBoxEventDisplay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxEventDisplay.FormattingEnabled = true;
			this.comboBoxEventDisplay.Location = new System.Drawing.Point(111, 3);
			this.comboBoxEventDisplay.Name = "comboBoxEventDisplay";
			this.comboBoxEventDisplay.Size = new System.Drawing.Size(121, 21);
			this.comboBoxEventDisplay.TabIndex = 0;
			// 
			// labelEventDisplayHint
			// 
			this.labelEventDisplayHint.AutoSize = true;
			this.labelEventDisplayHint.Location = new System.Drawing.Point(3, 6);
			this.labelEventDisplayHint.Name = "labelEventDisplayHint";
			this.labelEventDisplayHint.Size = new System.Drawing.Size(102, 13);
			this.labelEventDisplayHint.TabIndex = 1;
			this.labelEventDisplayHint.Text = "Event Display Mode";
			// 
			// listViewEventList
			// 
			this.listViewEventList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.listViewEventList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderStartPosition,
            this.columnHeaderData});
			this.listViewEventList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listViewEventList.Location = new System.Drawing.Point(6, 30);
			this.listViewEventList.Name = "listViewEventList";
			this.listViewEventList.Size = new System.Drawing.Size(272, 262);
			this.listViewEventList.TabIndex = 2;
			this.listViewEventList.UseCompatibleStateImageBehavior = false;
			this.listViewEventList.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderStartPosition
			// 
			this.columnHeaderStartPosition.Text = "Position";
			// 
			// columnHeaderData
			// 
			this.columnHeaderData.Text = "Data";
			this.columnHeaderData.Width = 165;
			// 
			// ListEditorControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.listViewEventList);
			this.Controls.Add(this.labelEventDisplayHint);
			this.Controls.Add(this.comboBoxEventDisplay);
			this.Name = "ListEditorControl";
			this.Size = new System.Drawing.Size(432, 295);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBoxEventDisplay;
		private System.Windows.Forms.Label labelEventDisplayHint;
		private System.Windows.Forms.ListView listViewEventList;
		private System.Windows.Forms.ColumnHeader columnHeaderStartPosition;
		private System.Windows.Forms.ColumnHeader columnHeaderData;
	}
}

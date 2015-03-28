using krassequenzer.MusicModel;
using krassequenzer.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using krassequenzer.Stuff;
using System.Diagnostics;

namespace krassequenzer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
			this.SuspendLayout();
            InitializeComponent();
			this.trackOverviewControl = new TrackOverviewControl();
			this.trackOverviewControl.Dock = DockStyle.Fill;
			this.panelTrackOverviewContainer.Controls.Add(this.trackOverviewControl);
			this.ResumeLayout();

			// this is the thing that needs to be passed to all other
			// views which need access to the application state
			var viewContext = new ViewContext();
			this.formController = new FormController(viewContext, this);
			this.trackOverviewControl.Context = this.Context;

			this.SetApplicationStatus("Ready");
        }

		private readonly FormController formController;
		private readonly TrackOverviewControl trackOverviewControl;
		
		/// <summary>
		/// Gets the invariant <see cref="ViewContext"/> of this application.
		/// </summary>
		public ViewContext Context
		{
			get { return this.formController.Context; }
		}

		/// <summary>
		/// Gets the window's currently active composition.
		/// </summary>
		public Composition CurrentComposition
		{
			get { return this.Context.CurrentComposition.Value; }
			private set { this.Context.CurrentComposition.Value = value; }
		}

		private void SetApplicationStatus(string message)
		{
			this.toolStripStatusLabelStatus.Text = message;
		}

		private void toolStripButtonUpdate_Click(object sender, EventArgs e)
		{
			this.trackOverviewControl.Rebuild();
		}

		private void CreateAndLoadNewComposition()
		{
			var composition = new Composition();
			this.CurrentComposition = composition;
		}

		private void toolStripMenuItemCompositionProperties_Click(object sender, EventArgs e)
		{
			this.formController.CompositionPropertiesFormManager.Show();
		}

		private void toolStripMenuItemNewComposition_Click(object sender, EventArgs e)
		{
			this.CreateAndLoadNewComposition();
		}

		private void toolStripMenuItemExit_Click(object sender, EventArgs e)
		{
			// do not add anything else in this method, ever
			this.Close();
		}

		private void toolStripMenuItemAddTrack_Click(object sender, EventArgs e)
		{
			this.CurrentComposition.Maybe(x => x.Tracks.Add(new Track()));
		}

		private void toolStripButtonDebugBreak_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Debugger.Break();
		}

		private void toolStripMenuItemDeviceSetup_Click(object sender, EventArgs e)
		{
			this.formController.DeviceSetupFormManager.Show();
		}

		private void toolStripMenuItemListEditor_Click(object sender, EventArgs e)
		{
			// TODO actually get the selected track
			var selectedTrack = this.CurrentComposition.Maybe(x => x.Tracks.FirstOrDefault());
			if (selectedTrack == null)
			{
				Debug.WriteLine("No track selected");
				return;
			}

			var form = new Form();
			form.Owner = this;
			var control = new ListEditorControl();
			control.Track = selectedTrack;
			control.Dock = DockStyle.Fill;
			
			control.ObjectPropertiesCaller = x => { this.Context.SelectedObject.Value = x; this.formController.ObjectPropertiesFormManager.Show(); };
			form.Controls.Add(control);
			form.Show();
		}
    }
}

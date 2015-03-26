﻿using krassequenzer.MusicModel;
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
			this.context = new ViewContext();
			this.trackOverviewControl.Context = this.context;

			this.compositionPropertiesFormManager = new ModelessDialogManager(this, this.CreateCompositionPropertiesForm);

			this.SetApplicationStatus("Ready");
        }

		private readonly ModelessDialogManager compositionPropertiesFormManager;
		private readonly ViewContext context;
		private readonly TrackOverviewControl trackOverviewControl;
		
		/// <summary>
		/// Gets the invariant <see cref="ViewContext"/> of this application.
		/// </summary>
		public ViewContext Context
		{
			get { return this.context; }
		}

		/// <summary>
		/// Gets the window's currently active composition.
		/// </summary>
		public Composition CurrentComposition
		{
			get { return this.context.CurrentComposition.Value; }
			private set { this.context.CurrentComposition.Value = value; }
		}

		private void SetApplicationStatus(string message)
		{
			this.toolStripStatusLabelStatus.Text = message;
		}

		private Form CreateCompositionPropertiesForm()
		{
			var form = new CompositionPropertiesForm();
			form.Context = this.Context;
			return form;
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
			this.compositionPropertiesFormManager.Show();
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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using krassequenzer.MusicModel;

using krassequenzer.Stuff;

namespace krassequenzer.Views
{
	public partial class TrackHeaderControl : UserControl
	{
		public TrackHeaderControl()
		{
			InitializeComponent();
		}

		private Track _track;
		public Track Track
		{
			get { return this._track; }
			set
			{
				this._track = value;
				this.RebuildControls();
			}
		}

		private void RebuildControls()
		{
			this.textBoxTrackName.Text = this.Track.Maybe(x => x.Name);
		}

		private void textBoxTrackName_TextChanged(object sender, EventArgs e)
		{
			// Update the track name immediately if the person does things
			this.Track.Maybe(x => x.Name = this.textBoxTrackName.Text);
		}
	}
}

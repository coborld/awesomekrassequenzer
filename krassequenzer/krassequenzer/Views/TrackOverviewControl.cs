using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using krassequenzer.Stuff;
using krassequenzer.MusicModel;

namespace krassequenzer.Views
{
	public partial class TrackOverviewControl : UserControl
	{
		public TrackOverviewControl()
		{
			InitializeComponent();
		}

		private ViewContext _context;
		
		public ViewContext Context
		{
			get { return this._context; }
			set
			{
				this._context.Maybe(x => x.CurrentComposition.ValueChanged -= this.HandleCurrentCompositionChanged);
				this._context = value;
				this.RebuildControls();
				this._context.Maybe(x => x.CurrentComposition.ValueChanged += this.HandleCurrentCompositionChanged);
			}
		}

		/// <summary>
		/// Gets the composition in the current <see cref="Context"/>. Can be null.
		/// </summary>
		public Composition CurrentComposition
		{
			get { return this.Context.Maybe(x => x.CurrentComposition.Value); }
		}

		private void HandleCurrentCompositionChanged(object sender, EventArgs e)
		{
			this.RebuildControls();
		}

		/// <summary>
		/// Forces an immediate GUI update.
		/// </summary>
		public void Rebuild()
		{
			this.RebuildControls();
		}

		private void RebuildControls()
		{
			if (this.CurrentComposition == null)
			{
				this.panelTrackContentContainer.Controls.Clear();
				return;
			}

			// we need to add one appropriate track header control and one track content control
			// for each track
			// y-coordinate of the stuff
			var y = 0;
			// we also need a fixed height for each track, so that we can set the header and the
			// content control to the same height. TODO: make this user-adjustable on a per-track
			// basis
			const int trackHeight = 100; // px
			foreach (var t in this.CurrentComposition.Tracks)
			{
				var header = new TrackHeaderControl();
				header.Track = t;
				header.Top = y;
				header.Height = trackHeight;
				header.Width = this.panelTrackHeaderContainer.Width;
				header.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
				this.panelTrackHeaderContainer.Controls.Add(header);

				var content = new TrackContentControl();
				//content.Track = t;
				content.Top = y;
				content.Height = trackHeight;
				content.Width = this.panelTrackContentContainer.Width;
				content.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
				this.panelTrackContentContainer.Controls.Add(content);

				y += trackHeight;
			}
		}
	}
}

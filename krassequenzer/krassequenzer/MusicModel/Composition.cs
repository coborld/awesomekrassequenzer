using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MusicModel
{
	/// <summary>
	/// Represents a musical composition.
	/// </summary>
	public class Composition
	{
		/// <summary>
		/// Gets or sets the title of the composition.
		/// </summary>
		public string Title { get; set; }

		private readonly ObservableCollection<Track> _tracks = new ObservableCollection<Track>();
		/// <summary>
		/// Gets a list of tracks, each representing one instrument or instrument group.
		/// </summary>
		public ObservableCollection<Track> Tracks { get { return this._tracks; } }

		private readonly TempoTrack _tempoTrack = new TempoTrack();
		/// <summary>
		/// Gets the tempo track of the composition. The tempo track denotes the tempo
		/// for all other tracks in the score.
		/// </summary>
		public TempoTrack TempoTrack { get { return this._tempoTrack; } }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("\"");
			sb.Append(Title);
			sb.Append("\"");
			sb.Append(Environment.NewLine);
			
			foreach (var track in Tracks)
			{
				sb.Append(Environment.NewLine);
				sb.Append(track.ToString());
			}

			sb.Append(TempoTrack.ToString());

			return sb.ToString();
		}
	}
}

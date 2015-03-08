using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MusicModel
{
	public class Score
	{
		public string Title { get; set; }

		private readonly List<Track> _tracks = new List<Track>();
		public List<Track> Tracks { get { return this._tracks; } }

		private readonly TempoTrack _tempoTrack = new TempoTrack();
		public TempoTrack TempoTrack { get { return this._tempoTrack; } }

		public string ToString()
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

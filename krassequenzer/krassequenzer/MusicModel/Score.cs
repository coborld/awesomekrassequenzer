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
	}
}

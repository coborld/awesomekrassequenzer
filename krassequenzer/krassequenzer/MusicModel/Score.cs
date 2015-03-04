using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MusicModel
{
	public class Score
	{
		private readonly List<Part> _parts = new List<Part>();
		public List<Part> Parts { get { return this._parts; } }

		private readonly TempoTrack _tempoTrack = new TempoTrack();
		public TempoTrack TempoTrack { get { return this._tempoTrack; } }
	}
}

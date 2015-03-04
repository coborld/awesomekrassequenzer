using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MusicModel
{
	public class TempoChange
	{
		public TempoChange(MusicalTime position, bool linearInterpolation, Tempo newTempo)
		{
			this._position = position;
			this._linearInterpolation = linearInterpolation;
			this._newTempo = newTempo;
		}

		private readonly MusicalTime _position;
		private readonly bool _linearInterpolation;
		private readonly Tempo _newTempo;

		public MusicalTime Position { get { return this._position; } }
		public bool LinearInterpolation { get { return this._linearInterpolation; } }
		public Tempo NewTempo { get { return this._newTempo; } }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using krassequenzer.Stuff;

namespace krassequenzer.MusicModel
{
	public struct TimeSignature
	{
		public TimeSignature(int beats, int beatUnit)
		{
			if (beats <= 0) throw new ArgumentOutOfRangeException("beats");
			if (!beatUnit.IsPowerOf2()) throw new ArgumentOutOfRangeException("beatUnit");
			this._beats = beats;
			this._beatUnit = beatUnit;
		}

		private readonly int _beats;
		private readonly int _beatUnit;

		public int Beats { get { return this._beats; } }
		public int BeatUnit { get { return this._beatUnit; } }

		public override string ToString()
		{
			return this.Beats + " / " + this.BeatUnit;
		}
	}
}

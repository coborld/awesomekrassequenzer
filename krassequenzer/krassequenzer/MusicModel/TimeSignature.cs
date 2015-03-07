using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using krassequenzer.Stuff;

namespace krassequenzer.MusicModel
{
	/// <summary>
	/// Represents a time signature, defined by beat unit and beats per measure.
	/// </summary>
	public class TimeSignature
	{
		/// <summary>
		/// Initializes a new instance.
		/// </summary>
		/// <param name="beats">How many notes of <paramref="beatUnit"/> are in
		/// one measure.</param>
		/// <param name="beatUnit">What type of note the beat is.</param>
		public TimeSignature(int beats, int beatUnit)
		{
			if (beats <= 0) throw new ArgumentOutOfRangeException("beats");
			if (!beatUnit.IsPowerOf2()) throw new ArgumentOutOfRangeException("beatUnit");
			this._beats = beats;
			this._beatUnit = beatUnit;
		}

		private readonly int _beats;
		private readonly int _beatUnit;

		/// <summary>
		/// Gets how many notes of <see cref="BeatUnit"/> are in one measure.
		/// </summary>
		public int Beats { get { return this._beats; } }
		/// <summary>
		/// Gets what type of note the beat is.
		/// </summary>
		public int BeatUnit { get { return this._beatUnit; } }

		/// <summary>
		/// Returns a string representation containing the <see cref="Beats"/> and
		/// <see cref="BeatUnit"/> properties.
		/// </summary>
		public override string ToString()
		{
			return this.Beats.ToStringIv() + " / " + this.BeatUnit.ToStringIv();
		}
	}
}

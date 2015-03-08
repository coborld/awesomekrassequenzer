using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MusicModel
{
	/// <summary>
	/// Represents the change of the time signature at a particular measure.
	/// </summary>
	public class TimeSignatureChange
	{
		/// <summary>
		/// Initializes a new instance.
		/// </summary>
		/// <param name="startMeasure">The measure from which the new time signature applies.
		/// Measures a counted from 0.</param>
		/// <param name="newTimeSignature">The new time signature.</param>
		public TimeSignatureChange(int startMeasure, TimeSignature newTimeSignature)
		{
			if (startMeasure < 0) throw new ArgumentOutOfRangeException("startMeasure", "Start measure cannot be negative.");
			this._startMeasure = startMeasure;
			this._newTimeSignature = newTimeSignature;
		}

		private readonly int _startMeasure;
		private readonly TimeSignature _newTimeSignature;

		/// <summary>
		/// Gets the measure from which the new time signature applies.
		/// Measures are counted from 0.
		/// </summary>
		public int StartMeasure { get { return this._startMeasure; } }
		/// <summary>
		/// Gets the new time signature.
		/// </summary>
		public TimeSignature NewTimeSignature { get { return this._newTimeSignature; } }
	}
}

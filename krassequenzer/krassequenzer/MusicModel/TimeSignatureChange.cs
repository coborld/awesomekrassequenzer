using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MusicModel
{
	public class TimeSignatureChange
	{
		public TimeSignatureChange(int startMeasure, TimeSignature newTimeSignature)
		{
			this._startMeasure = startMeasure;
			this._newTimeSignature = newTimeSignature;
		}

		private readonly int _startMeasure;
		private readonly TimeSignature _newTimeSignature;

		public int Time { get { return this._startMeasure; } }
		public TimeSignature NewTimeSignature { get { return this._newTimeSignature; } }
	}
}

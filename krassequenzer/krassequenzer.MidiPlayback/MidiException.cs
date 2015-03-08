using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MidiPlayback
{
	public class MidiException : Exception
	{
		public MidiException()
		{
		}

		public MidiException(string message)
			: base(message)
		{
		}

		public MidiException(string message, int mmsyserr)
			: base(message)
		{
			this._mmsyserr = mmsyserr;
		}

		private readonly int _mmsyserr;
		public int Mmsyserr { get { return this._mmsyserr; } }

		public MidiException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		public MidiException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

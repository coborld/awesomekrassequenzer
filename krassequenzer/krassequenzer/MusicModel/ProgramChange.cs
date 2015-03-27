using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using krassequenzer.Stuff;

namespace krassequenzer.MusicModel
{
	public class ProgramChange
	{
		public int Instrument { get; set; }
		public StreamTime Time { get; set; }

		/// <summary>
		/// Specifies the MIDI channel index for this instance. If the value of this property is null,
		/// the default channel for the <see cref="Track"/> is used.
		/// </summary>
		public MidiChannelIndex? MidiChannelIndex { get; set; }

		public static int TimeComparison(ProgramChange x, ProgramChange y)
		{
			return StreamTime.Comparison(x.NotNull("x").Time, y.NotNull("y").Time);
		}
	}
}

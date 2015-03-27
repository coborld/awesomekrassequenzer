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
		public MusicalTime Time { get; set; }
		public MidiChannelIndex MidiChannelIndex { get; set; }

		public static int TimeComparison(ProgramChange x, ProgramChange y)
		{
			return MusicalTime.Comparison(x.NotNull("x").Time, y.NotNull("y").Time);
		}
	}
}

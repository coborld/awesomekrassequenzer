using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MidiPlayback
{
	public class MidiStreamEvent
	{
		public MidiStreamEvent(int deltaTime, int data)
		{
			this.DeltaTime = deltaTime;
			this.Data = data;
		}

		public int DeltaTime { get; set; }
		public int Data { get; set; }
	}
}

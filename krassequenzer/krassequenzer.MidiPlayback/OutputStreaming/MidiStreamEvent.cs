using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MidiPlayback
{
	public struct MidiStreamEvent
	{
		public MidiStreamEvent(uint deltaTime, uint data)
		{
			this._deltaTime = deltaTime;
			this._data = data;
		}

		private readonly uint _deltaTime;
		private readonly uint _data;

		public uint DeltaTime { get { return this._deltaTime; } }
		public uint Data { get { return this._data; } }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MidiPlayback
{
	public class MidiOutClient
	{
		public MidiOutClient(MidiOutDevice device)
		{
			if (device == null) throw new ArgumentNullException("device");
#warning TODO change to IMidiOutDevice
			this.device = device;
		}

		private readonly MidiOutDevice device;

		public void NoteOn(int channel, int key, int velocity)
		{
			uint u = 0x90;
			u |= (uint)channel & 0xf;
			u |= ((uint)key & 0x7f) << 8;
			u |= ((uint)velocity & 0x7f) << 16;
			this.device.ShortMessage(u);
		}

		public void NoteOff(int channel, int key, int velocity)
		{
			uint u = 0x80;
			u |= (uint)channel & 0xf;
			u |= ((uint)key & 0x7f) << 8;
			u |= ((uint)velocity & 0x7f) << 16;
			this.device.ShortMessage(u);
		}

		public void ControlChange(int channel, int controller, int value)
		{
			uint u = 0xb0;
			u |= (uint)channel & 0xf;
			u |= ((uint)controller & 0x7f) << 8;
			u |= ((uint)value & 0x7f) << 16;
			this.device.ShortMessage(u);
		}
		
		public void ProgramChange(int channel, int program)
		{
			uint u = 0xc0;
			u |= (uint)channel & 0xf;
			u |= ((uint)program & 0x7f) << 8;
			this.device.ShortMessage(u);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="channel"></param>
		/// <param name="value">Range -8192 .. 8191</param>
		public void PitchBend(int channel, int value)
		{
			uint u = 0xe0;
			value += 0x2000;
			u |= ((uint)value & 0x7f) << 8;
			u |= (((uint)value >> 7) & 0x7f) << 16;
			this.device.ShortMessage(u);
		}
	}
}

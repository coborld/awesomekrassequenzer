using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MidiPlayback.OutputStreaming
{
	public class MidiStreamEventFactory
	{
		public MidiStreamEvent NoteOn(uint deltaTime, int channel, int key, int velocity)
		{
			uint u = MidiMessageBuilder.NoteOn(channel, key, velocity);
			var e = new MidiStreamEvent(deltaTime, u);
			return e;
		}

		public MidiStreamEvent NoteOff(uint deltaTime, int channel, int key, int velocity)
		{
			uint u = MidiMessageBuilder.NoteOff(channel, key, velocity);
			var e = new MidiStreamEvent(deltaTime, u);
			return e;
		}

		public MidiStreamEvent ProgramChange(uint deltaTime, int channel, int program)
		{
			uint u = MidiMessageBuilder.ProgramChange(channel, program);
			var e = new MidiStreamEvent(deltaTime, u);
			return e;
		}

		public MidiStreamEvent ControlChange(uint deltaTime, int channel, int controller, int value)
		{
			uint u = MidiMessageBuilder.ControlChange(channel, controller, value);
			var e = new MidiStreamEvent(deltaTime, u);
			return e;
		}

		public MidiStreamEvent ChannelPressure(uint deltaTime, int channel, int value)
		{
			uint u = MidiMessageBuilder.ChannelPressure(channel, value);
			var e = new MidiStreamEvent(deltaTime, u);
			return e;
		}

		public MidiStreamEvent PolyphonicKeyPressure(uint deltaTime, int channel, int key, int value)
		{
			uint u = MidiMessageBuilder.PolyphonicKeyPressure(channel, key, value);
			var e = new MidiStreamEvent(deltaTime, u);
			return e;
		}
	}
}

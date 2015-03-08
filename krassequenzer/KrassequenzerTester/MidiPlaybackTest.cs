using krassequenzer.MidiPlayback;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrassequenzerTester
{
	class MidiPlaybackTest
	{
		public static void Run()
		{
			var systemInfo = MidiSystemInfo.Query();
			var msSynth = systemInfo.OutDeviceInfo.Single(x => x.Name.Contains("Microsoft"));
			
#if false
			using (var device = new MidiOutDevice(msSynth))
			{
				device.Open();

				var client = new MidiOutClient(device);

				client.ProgramChange(0, 49);
				client.ControlChange(0, 0xa, 80);
				System.Threading.Thread.Sleep(500);
				client.NoteOn(0, 60, 64);
				client.NoteOn(0, 67, 64);
				System.Threading.Thread.Sleep(250);
				client.PitchBend(0, -0x1000);
				client.ControlChange(0, 0xa, 40);
				System.Threading.Thread.Sleep(250);
				client.NoteOff(0, 60, 64);
				client.NoteOff(0, 67, 64);
				System.Threading.Thread.Sleep(500);
			}

			Debug.WriteLine("midi playback test ended");
#endif

			using (var stream = new MidiOutStream(msSynth))
			{
				//stream.Open();
				stream.Play();
			}

			Debug.WriteLine("midi stream test ended");
		}
	}
}

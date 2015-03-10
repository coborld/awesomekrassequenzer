using krassequenzer.MidiPlayback;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
				//stream.Test();
				stream.Open();

				var f = new MidiStreamEventFactory();

				const int timeDiv = 24;

				const int c = 60;
				f.ProgramChange(0, 0, (int)MidiGMInstrumentSet.String_Ensemble_1);
				f.ControlChange(0, 0, 0xa, 30);
				f.ProgramChange(0, 1, (int)MidiGMInstrumentSet.String_Ensemble_1);
				f.ControlChange(0, 1, 0xa, 80);
				
				f.NoteOn(0, 0, c + 0, 80);
				f.NoteOff(24, 0, c + 0, 80);
				f.NoteOn(0, 0, c + 5, 80);
				f.NoteOff(24, 0, c + 5, 80);
				f.NoteOn(0, 0, c + 12, 80);
				f.NoteOff(24, 0, c + 12, 80);

				f.NoteOn(0, 0, c + 12, 80);
				f.NoteOn(0, 1, c - 23, 64);
				f.NoteOn(0, 1, c - 16, 64);
				f.NoteOff(24, 0, c + 12, 80);
				f.NoteOff(0, 1, c - 23, 80);
				f.NoteOff(0, 1, c - 16, 80);
				f.NoteOn(0, 0, c + 10, 80);
				f.NoteOn(0, 1, c - 11, 64);
				f.NoteOn(0, 1, c - 7, 64);
				f.NoteOn(0, 1, c - 4, 64);
				f.NoteOff(24, 1, c - 11, 80);
				f.NoteOff(0, 1, c - 7, 80);
				f.NoteOff(0, 1, c - 4, 80);
				f.NoteOn(0, 1, c - 9, 64);
				f.NoteOn(0, 1, c - 5, 64);
				f.NoteOn(0, 1, c - 2, 64);

				f.NoteOff(24, 0, c + 10, 80);
				f.NoteOn(0, 0, c + 8, 80);
				f.NoteOff(12, 0, c + 8, 80);
				f.NoteOn(0, 0, c + 7, 80);
				f.NoteOff(12, 0, c + 7, 80);
				f.NoteOff(0, 1, c - 9, 80);
				f.NoteOff(0, 1, c - 5, 80);
				f.NoteOff(0, 1, c - 2, 80);

				f.NoteOn(0, 0, c + 0, 80);
				f.NoteOn(0, 1, c - 19, 64);
				f.NoteOn(0, 1, c - 12, 64);
				f.NoteOff(12, 0, c + 0, 80);
				f.NoteOn(0, 0, c + 3, 80);
				f.NoteOff(12, 0, c + 3, 80);
				f.NoteOff(0, 1, c - 19, 64);
				f.NoteOff(0, 1, c - 12, 64);
				f.NoteOn(0, 0, c + 5, 80);
				f.NoteOn(0, 1, c - 12, 80);
				f.NoteOn(0, 1, c - 7, 80);
				f.NoteOn(0, 1, c - 2, 80);
				f.NoteOff(24, 1, c - 12, 80);
				f.NoteOff(0, 1, c - 7, 80);
				f.NoteOff(0, 1, c - 2, 80);
				f.NoteOn(0, 1, c - 12, 80);
				f.NoteOn(0, 1, c - 7, 80);
				f.NoteOn(0, 1, c - 4, 80);

				// the fade is real SO SMOOTH SO GOOD
				f.ControlChange(12, 0, 7, 90);
				f.ControlChange(0, 1, 7, 90);
				f.ControlChange(12, 0, 7, 60);
				f.ControlChange(0, 1, 7, 60);
				f.ControlChange(12, 0, 7, 30);
				f.ControlChange(0, 1, 7, 30);

				f.NoteOff(12, 0, c + 5, 80);
				f.NoteOff(0, 1, c - 12, 80);
				f.NoteOff(0, 1, c - 7, 80);
				f.NoteOff(0, 1, c - 4, 80);

				stream.SetTimeDiv(timeDiv);
				stream.SetTempo(1000000);
				var cts = new CancellationTokenSource();
				var playTask = stream.Play(f.Events, cts.Token);
				stream.RestartPlayback();
				playTask.Wait();

				// wait for the TAIL
				Thread.Sleep(500);
			}

			Debug.WriteLine("midi stream test ended");
		}
	}
}

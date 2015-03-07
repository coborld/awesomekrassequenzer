using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
	class Program
	{
		static void Main(string[] args)
		{
			using (TimerResolution.Install(1))
			{
				var numDevices = NativeMethods.midiOutGetNumDevs();
				Debug.WriteLine(numDevices);

				for (int i = 0; i <= numDevices; ++i)
				{
					MidiOutCaps caps = new MidiOutCaps();
					int result = NativeMethods.midiOutGetDevCaps(new IntPtr(i), ref caps, (uint)MemoryUtil.SizeOfMidiOutCaps);
					Debug.WriteLine(result);
					Debug.WriteLine(caps.Name);
					Debug.WriteLine(GetError(result));
				}

				var midiSystemInfo = MidiSystemInfo.Query();
				foreach (var info in midiSystemInfo.OutDeviceInfo)
				{
					Debug.WriteLine(info.Caps.Name);
				}

				var goodInfo = midiSystemInfo.OutDeviceInfo.Single(x => x.Caps.Name == "Microsoft GS Wavetable Synth");
				
				MidiOutSafeHandle handle;
				var openResult = NativeMethods.midiOutOpen(out handle, (uint)goodInfo.Id, MidiProcTest, IntPtr.Zero, NativeMethods.CALLBACK_FUNCTION);
				CheckThrow(openResult);
				try
				{
#if false
					//NativeMethods.midiOutShortMsg(handle, 0x91335500);
					NativeMethods.midiOutShortMsg(handle, 0x00553391);
					Thread.Sleep(1000);
					//NativeMethods.midiOutShortMsg(handle, 0x81335500);
					NativeMethods.midiOutShortMsg(handle, 0x00553381);
					Thread.Sleep(500);
#endif

					Thread.Sleep(500);

					const int c = 60;
					int lastNote = 0;
					var sw = Stopwatch.StartNew();
					int supposedTime = 0;
					Action<int, int> note = (k, t) =>
						{
							if (lastNote != 0)
							{
								NativeMethods.midiOutShortMsg(handle, BuildNoteMessage(false, 0, lastNote + 12, 64));
								//NativeMethods.midiOutShortMsg(handle, BuildNoteMessage(false, 2, lastNote + 12, 64));
							}
							lastNote = k;
							NativeMethods.midiOutShortMsg(handle, BuildNoteMessage(true, 0, k + 12, 64));
							var inaccuracy = (int)sw.ElapsedMilliseconds - supposedTime;
							Console.WriteLine("time inaccuracy: " + inaccuracy + "ms");
							//NativeMethods.midiOutShortMsg(handle, BuildNoteMessage(true, 2, k + 12, 64));
							var sleeping = 350 * t;
							supposedTime += sleeping;
							Thread.Sleep(sleeping - inaccuracy);
						};

					Action<bool, int> asyncNote = (b, k) =>
						{
							NativeMethods.midiOutShortMsg(handle, BuildNoteMessage(b, 1, c + k, 50));
							NativeMethods.midiOutShortMsg(handle, BuildNoteMessage(b, 2, c + k + 12, 50));
						};

					NativeMethods.midiOutShortMsg(handle, BuildProgramChange(0, 75));
					NativeMethods.midiOutShortMsg(handle, BuildProgramChange(1, 49));
					NativeMethods.midiOutShortMsg(handle, BuildProgramChange(2, 49));

					NativeMethods.midiOutShortMsg(handle, BuildCc(1, 0x0a, 90));
					NativeMethods.midiOutShortMsg(handle, BuildCc(2, 0x0a, 30));

					note(c + 3, 1);
					note(c + 5, 1);

					asyncNote(true, -16);
					asyncNote(true, -9);

					note(c + 7, 2);
					note(c + 8, 1);
					note(c + 7, 1);

					asyncNote(false, -16);
					asyncNote(false, -9);
					asyncNote(true, -14);
					asyncNote(true, -7);

					note(c + 5, 3);
					note(c + 3, 1);

					asyncNote(false, -14);
					asyncNote(false, -7);
					asyncNote(true, -12);
					asyncNote(true, -5);

					note(c + 3, 1);
					note(c + 2, 1);
					note(c + 0, 4);
					note(c + 0, 1);
					note(c + 2, 1);

					asyncNote(false, -12);
					asyncNote(false, -5);
					asyncNote(true, -16);
					asyncNote(true, -9);

					note(c + 3, 2);
					note(c + 2, 1);
					note(c + 0, 1);

					asyncNote(false, -16);
					asyncNote(false, -9);
					asyncNote(true, -14);
					asyncNote(true, -10);

					note(c + 2, 2);
					note(c + 5, 2);

					asyncNote(false, -14);
					asyncNote(false, -10);
					asyncNote(true, -12);
					asyncNote(true, -17);

					note(c + 0, 8);
		
					asyncNote(false, -12);
					asyncNote(false, -17);

					NativeMethods.midiOutShortMsg(handle, BuildNoteMessage(false, 0, c + 12, 64));

					Console.WriteLine();
					Console.WriteLine();
					Console.WriteLine();
					Console.WriteLine();
					Console.WriteLine();
					Console.WriteLine();
					Console.WriteLine();
					Console.WriteLine();
					Console.WriteLine();
					Console.WriteLine();
					Console.WriteLine("                                   ~ F I N ~");
					Console.WriteLine();
					Console.WriteLine();
					Console.WriteLine();
					Console.WriteLine();
					Console.WriteLine();
					Console.WriteLine();
					Console.WriteLine();
					Console.WriteLine();
					Console.WriteLine();
					Console.WriteLine();

					Thread.Sleep(1000);
				}
				finally
				{
					handle.Close();
				}
			}
		}

		static uint BuildNoteMessage(bool noteOn, int channel, int key, int velocity)
		{
			Console.WriteLine("noteOn: " + noteOn + "; channel: " + channel + "; key: " + key + "; velocity: " + velocity);

			uint u = 0x00000080;
			if (noteOn)
			{
				u |= 0x10;
			}
			u |= (uint)channel & 0xf;
			u |= ((uint)key & 0x7f) << 8;
			u |= ((uint)velocity & 0x7f) << 16;
			return u;
		}

		static uint BuildCc(int channel, int cc, int value)
		{
			Console.WriteLine("CC / channel: " + channel + "; cc: " + cc + "; value: " + value);

			uint u = 0x000000b0;
			u |= (uint)channel & 0xf;
			u |= ((uint)cc & 0x7f) << 8;
			u |= ((uint)value & 0x7f) << 16;
			return u;
		}

		static uint BuildProgramChange(int channel, int program)
		{
			Console.WriteLine("Program Change / channel: " + channel + "; program: " + program);

			uint u = 0x000000c0;
			u |= (uint)channel & 0xf;
			u |= ((uint)program & 0x7f) << 8;
			return u;
		}

		static void CheckThrow(int mmsyserr)
		{
			if (mmsyserr != NativeMethods.MMSYSERR_NOERROR)
			{
				var errString = GetError(mmsyserr);
				throw new Exception(errString);
			}
		}

		static void MidiProcTest(IntPtr handle, uint msg, IntPtr instance, IntPtr param1, IntPtr param2)
		{

		}

		static string GetError(int result)
		{
			StringBuilder errorTextBuilder = new StringBuilder(NativeMethods.MAXERRORLENGTH);
			var retval = NativeMethods.midiOutGetErrorText(result, errorTextBuilder, errorTextBuilder.Capacity);
			return errorTextBuilder.ToString();
		}
	}
}

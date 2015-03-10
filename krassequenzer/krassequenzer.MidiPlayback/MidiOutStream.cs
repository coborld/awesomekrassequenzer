using krassequenzer.MidiPlayback.Low;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace krassequenzer.MidiPlayback
{
	public class MidiOutStream : IMidiOutDevice, IDisposable
	{
		public MidiOutStream(MidiOutDeviceInfo info)
		{
			if (info == null) throw new ArgumentNullException("info");
			this._info = info;
		}

		private readonly MidiOutDeviceInfo _info;

		public MidiOutDeviceInfo Info { get { return this._info; } }

		private MidiOutStreamSafeHandle currentHandle;

		public void Dispose()
		{
			this.Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			if (this.currentHandle != null)
			{
				Debug.WriteLine("Disposing midi out stream safe handle...");
				this.currentHandle.Dispose();
				Debug.WriteLine("Midi out stream safe handle disposed.");
				this.currentHandle = null;
			}
		}

		public void Open()
		{
			MidiOutStreamSafeHandle handle;
			Debug.WriteLine("Opening midi stream...");
			var deviceId = this.Info.Id;
			var result = NativeMethods.midiStreamOpen(out handle, ref deviceId, 1, this.MidiProc, IntPtr.Zero, NativeMethods.CALLBACK_FUNCTION);
			NativeMethods.CheckMidiOutMmsyserr(result);
			Debug.WriteLine("Midi stream opened.");
			this.currentHandle = handle;
		}

		public void RestartPlayback()
		{
			var result = NativeMethods.midiStreamRestart(this.currentHandle);
			NativeMethods.CheckMidiOutMmsyserr(result);
		}

		private const int MaximumBufferSize = 16; // 16000 = 64k / 4 in ints

		public async Task Play(IEnumerable<MidiStreamEvent> events, CancellationToken ct)
		{
			// prepare the events into memory
			if (events == null)
			{
				throw new ArgumentNullException("events");
			}

			var bytes = new List<uint>();
			foreach (var e in events)
			{
				bytes.Add((uint)e.DeltaTime);
				bytes.Add(0);
				bytes.Add((uint)e.Data);
			}

			var requiredMemory = bytes.Count * 4;
			using (var memoryManager = new UnmanagedMemoryManager())
			{
				var headerMemory = memoryManager.Alloc(InteropStructSizes.SizeOfMidiHeader);
				var eventBufferMemory = memoryManager.Alloc(requiredMemory);

				MidiHeader header = new MidiHeader();
				header.Data = eventBufferMemory;
				header.BufferLength = (uint)requiredMemory;
				header.BytesRecorded = (uint)requiredMemory;

				Marshal.StructureToPtr(header, headerMemory, false);
				for (int i = 0; i < bytes.Count; ++i)
				{
					Marshal.WriteInt32(eventBufferMemory, i * 4, (int)bytes[i]);
				}

				int mmsyserr;

				mmsyserr = NativeMethods.midiOutPrepareHeader(this.currentHandle, headerMemory, (uint)InteropStructSizes.SizeOfMidiHeader);
				NativeMethods.CheckMidiOutMmsyserr(mmsyserr);

				this.mre.Reset();

				mmsyserr = NativeMethods.midiStreamOut(this.currentHandle, headerMemory, (uint)InteropStructSizes.SizeOfMidiHeader);
				NativeMethods.CheckMidiOutMmsyserr(mmsyserr);

				bool canceled = false;

				await Task.Run(() =>
					{
						try
						{
							this.mre.Wait(ct);
						}
						catch (OperationCanceledException)
						{
							canceled = true;
						}
					});

				if (canceled)
				{
					mmsyserr = NativeMethods.midiStreamStop(this.currentHandle);
					NativeMethods.CheckMidiOutMmsyserr(mmsyserr);
				}

				mmsyserr = NativeMethods.midiOutUnprepareHeader(this.currentHandle, headerMemory, (uint)InteropStructSizes.SizeOfMidiHeader);
				NativeMethods.CheckMidiOutMmsyserr(mmsyserr);
			}
		}

		public void Test()
		{
			int mmsyserr;

			var bytes = new byte[24];

			using (var bufferMemManager = new UnmanagedMemoryManager())
			{
				IntPtr bufferMem = bufferMemManager.Alloc(bytes.Length);

				MidiHeader header = new MidiHeader();
				header.Data = bufferMem; // TODO
				header.BufferLength = 24;
				header.BytesRecorded = 24;
				header.UserData = IntPtr.Zero;
				header.Flags = 0;
				header.Next = IntPtr.Zero;
				header.Reserved = 0;
				header.Offset = 0;
				//header.Reserved2 = new IntPtr[8];
				//for (int i = 0; i < header.Reserved2.Length; ++i)
				//{
				//	header.Reserved2[i] = IntPtr.Zero;
				//}

				// flags and event code
				bytes[8] = 0x90;
				bytes[9] = 63;
				bytes[10] = 0x55;
				bytes[11] = 0;
				// next event
				bytes[12] = 48; // delta time?
				bytes[20] = 0x80;
				bytes[21] = 63;
				bytes[22] = 0x55;
				bytes[23] = 0;

				Marshal.Copy(bytes, 0, bufferMem, bytes.Length);

				using (var headerMemManager = new UnmanagedMemoryManager())
				{
					var headerMem = headerMemManager.Alloc(InteropStructSizes.SizeOfMidiHeader);
					Marshal.StructureToPtr(header, headerMem, false);


					MidiOutStreamSafeHandle handle;
					Debug.WriteLine("Opening midi stream...");
					var deviceId = this.Info.Id;
					var result = NativeMethods.midiStreamOpen(out handle, ref deviceId, 1, this.MidiProc, IntPtr.Zero, NativeMethods.CALLBACK_FUNCTION);
					NativeMethods.CheckMidiOutMmsyserr(result);
					Debug.WriteLine("Midi stream opened.");
					this.currentHandle = handle;

					MidiOutStreamPortProperty prop = new MidiOutStreamPortProperty(0);
					mmsyserr = NativeMethods.midiStreamProperty(this.currentHandle, ref prop, NativeMethods.MIDIPROP_GET | NativeMethods.MIDIPROP_TIMEDIV);
					NativeMethods.CheckMidiOutMmsyserr(mmsyserr);
					Debug.WriteLine("timediv setting: " + prop.PropertyValue);
					mmsyserr = NativeMethods.midiStreamProperty(this.currentHandle, ref prop, NativeMethods.MIDIPROP_GET | NativeMethods.MIDIPROP_TEMPO);
					NativeMethods.CheckMidiOutMmsyserr(mmsyserr);
					Debug.WriteLine("tempo setting: " + prop.PropertyValue + " (" + 60000000.0 / (double)prop.PropertyValue + " QPM)");

					mmsyserr = NativeMethods.midiStreamRestart(this.currentHandle);
					NativeMethods.CheckMidiOutMmsyserr(mmsyserr);

					mmsyserr = NativeMethods.midiOutPrepareHeader(this.currentHandle, headerMem, (uint)InteropStructSizes.SizeOfMidiHeader);
					NativeMethods.CheckMidiOutMmsyserr(mmsyserr);

					mmsyserr = NativeMethods.midiStreamOut(this.currentHandle, headerMem, (uint)InteropStructSizes.SizeOfMidiHeader);
					NativeMethods.CheckMidiOutMmsyserr(mmsyserr);

					if (!this.mre.Wait(5000))
					{
						Debug.WriteLine("??");
					}

					//Thread.Sleep(1000);
					//header.Flags = 9;

					mmsyserr = NativeMethods.midiOutUnprepareHeader(this.currentHandle, headerMem, (uint)InteropStructSizes.SizeOfMidiHeader);
					if (mmsyserr != 0)
					{
						Debug.WriteLine(" BUGGERED: " + mmsyserr);
					}
					NativeMethods.CheckMidiOutMmsyserr(mmsyserr);
				}
			}
		}

		private readonly ManualResetEventSlim mre = new ManualResetEventSlim(false);

		private void MidiProc(IntPtr handle, uint msg, IntPtr instance, IntPtr param1, IntPtr param2)
		{
			if (msg == 0x3C9)
			{
				Debug.WriteLine("MOM_DONE");
				mre.Set();
			}
			Debug.WriteLine("midi proc?");
		}
	}
}

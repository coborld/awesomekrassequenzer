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
	/// <summary>
	/// Represents an output device that can play a continuous stream
	/// of midi messages.
	/// This class is not thread-safe.
	/// </summary>
	public class MidiOutStream : IMidiOutDevice, IDisposable
	{
		public MidiOutStream(MidiOutDeviceInfo info)
		{
			if (info == null) throw new ArgumentNullException("info");
			this._info = info;
		}

		private readonly MidiOutDeviceInfo _info;

		public MidiOutDeviceInfo Info { get { return this._info; } }

		private MidiOutStreamSafeHandle _currentHandle;

		/// <summary>
		/// Gets a value indicating whether the device associated with this
		/// instance is open.
		/// </summary>
		public bool IsOpen
		{
			get { return this._currentHandle != null; }
		}

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
			this.Close();
		}

		public void Open()
		{
			if (this._currentHandle != null)
			{
				throw new InvalidOperationException(this.GetType().Name + " cannot be opened: The instance is already open.");
			}

			MidiOutStreamSafeHandle handle;
			Debug.WriteLine("Opening midi stream...");
			var deviceId = this.Info.Id;
			var result = NativeMethods.midiStreamOpen(out handle, ref deviceId, 1, this.MidiProc, IntPtr.Zero, NativeMethods.CALLBACK_FUNCTION);
			NativeMethods.CheckMidiOutMmsyserr(result);
			this._currentHandle = handle;
			Debug.WriteLine("Midi stream opened successfully.");
		}

		public void Close()
		{
			if (this._currentHandle != null)
			{
				Debug.WriteLine("Disposing midi out stream safe handle...");
				this._currentHandle.Dispose();
				Debug.WriteLine("Midi out stream safe handle disposed.");
				this._currentHandle = null;
			}
		}

		private MidiOutStreamSafeHandle GetHandleOrThrow()
		{
			var handle = this._currentHandle;
			if (handle == null)
			{
				throw new InvalidOperationException("Cannot complete the operation because the " + this.GetType().Name + " is not open.");
			}
			return handle;
		}

		public void RestartPlayback()
		{
			var handle = this.GetHandleOrThrow();
			var result = NativeMethods.midiStreamRestart(handle);
			NativeMethods.CheckMidiOutMmsyserr(result);
		}

		private const int MaximumBufferSize = 16; // 16000 = 64k / 4 in ints

		private uint GetStreamProperty(uint propMask)
		{
			var handle = this.GetHandleOrThrow();
			var prop = new MidiOutStreamPortProperty(0);
			var result = NativeMethods.midiStreamProperty(handle, ref prop, NativeMethods.MIDIPROP_GET | propMask);
			NativeMethods.CheckMidiOutMmsyserr(result);
			return prop.PropertyValue;
		}

		private void SetStreamProperty(uint propMask, uint value)
		{
			var handle = this.GetHandleOrThrow();
			var prop = new MidiOutStreamPortProperty(value);
			var result = NativeMethods.midiStreamProperty(handle, ref prop, NativeMethods.MIDIPROP_SET | propMask);
			NativeMethods.CheckMidiOutMmsyserr(result);
		}

		// TODO use better return values and stuff for all these methods

		public MidiStreamTimeDivSetting GetTimeDiv()
		{
			var u = this.GetStreamProperty(NativeMethods.MIDIPROP_TIMEDIV);
			return new MidiStreamTimeDivSetting((int)u);
		}

		public void SetTimeDiv(MidiStreamTimeDivSetting value)
		{
			this.SetStreamProperty(NativeMethods.MIDIPROP_TIMEDIV, (uint)value.Value);
		}

		public MidiStreamTempoSetting GetTempo()
		{
			var u = this.GetStreamProperty(NativeMethods.MIDIPROP_TEMPO);
			return new MidiStreamTempoSetting((int)u);
		}

		public void SetTempo(MidiStreamTempoSetting value)
		{
			this.SetStreamProperty(NativeMethods.MIDIPROP_TEMPO, (uint)value.Value);
		}

		public async Task Play(IEnumerable<IMidiStreamEvent> events, CancellationToken ct)
		{
			// prepare the events into memory
			if (events == null)
			{
				throw new ArgumentNullException("events");
			}

			var handle = this.GetHandleOrThrow();

			var bytes = new List<uint>();

			// testing tempo
			//bytes.Add(0);
			//bytes.Add(0);
			//bytes.Add(0x80000006);
			//bytes.Add(0x070351ff);
			//bytes.Add(0x20a10000);
			//bytes.Add(0);
			//bytes.Add(0);
			//bytes.Add(0x010720a1);
			//bytes.Add(0);
			//bytes.Add(0);
			//bytes.Add(0x80000006);
			//bytes.Add(0x070351ff);
			//bytes.Add(0x0000a120);

			foreach (var e in events)
			{
				bytes.Add((uint)e.DeltaTime);
				bytes.Add(0);
				if (e is MidiStreamEvent)
				{
					var e1 = (MidiStreamEvent)e;
					bytes.Add((uint)e1.Data);
				}
				else if (e is MidiStreamEventLong)
				{
					var e1 = (MidiStreamEventLong)e;
					bytes.Add(0x80000000 | ((uint)e1.Length & 0xffffff));
					for (int i = 0; i < e1.Length; i += 4)
					{
						bytes.Add(0);
					}
				}
				else if (e is TempoChangeMidiStreamEvent)
				{
					var e1 = (TempoChangeMidiStreamEvent)e;
					uint u = 0x01000000 | ((uint)e1.Tempo & 0xffffff);
					bytes.Add(u);
				}
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

				mmsyserr = NativeMethods.midiOutPrepareHeader(handle, headerMemory, (uint)InteropStructSizes.SizeOfMidiHeader);
				NativeMethods.CheckMidiOutMmsyserr(mmsyserr);

				this.mre.Reset();

				mmsyserr = NativeMethods.midiStreamOut(handle, headerMemory, (uint)InteropStructSizes.SizeOfMidiHeader);
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
					mmsyserr = NativeMethods.midiStreamStop(handle);
					NativeMethods.CheckMidiOutMmsyserr(mmsyserr);
				}

				mmsyserr = NativeMethods.midiOutUnprepareHeader(handle, headerMemory, (uint)InteropStructSizes.SizeOfMidiHeader);
				NativeMethods.CheckMidiOutMmsyserr(mmsyserr);
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

#warning TODO remove
#if false
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
#endif

	}

	public struct MidiStreamTimeDivSetting
	{
		public MidiStreamTimeDivSetting(int value)
		{
			if (value < 0 || value > 0x7fff) throw new ArgumentOutOfRangeException("value");
			this._value = value;
		}

		private readonly int _value;
		/// <summary>
		/// Gets the MIDI time division setting in ticks per beat unit.
		/// </summary>
		public int Value { get { return this._value; } }

		public static implicit operator MidiStreamTimeDivSetting(int value)
		{
			return new MidiStreamTimeDivSetting(value);
		}
	}

	public struct MidiStreamTempoSetting
	{
		public MidiStreamTempoSetting(int value)
		{
			if (value < 0 || value > 0xffffff) throw new ArgumentOutOfRangeException("value");
			this._value = value;
		}

		private readonly int _value;
		/// <summary>
		/// Gets the MIDI tempo setting, in microsettings per beat.
		/// </summary>
		public int Value { get { return this._value; } }

		public static implicit operator MidiStreamTempoSetting(int value)
		{
			return new MidiStreamTempoSetting(value);
		}
	}

	public interface IMidiStreamEvent
	{
		uint DeltaTime { get; }
	}

	public struct MidiStreamEvent : IMidiStreamEvent
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

	public struct MidiStreamEventLong : IMidiStreamEvent
	{
		public MidiStreamEventLong(uint deltaTime, byte[] data)
		{
			this._deltaTime = deltaTime;
			this._data = data.ToArray();
		}

		private readonly uint _deltaTime;
		private readonly byte[] _data;
		public uint DeltaTime { get { return this._deltaTime; } }
		public int Length { get { return this._data.Length; } }
		public byte[] Data { get { return this._data; } }
	}

	public struct TempoChangeMidiStreamEvent : IMidiStreamEvent
	{
		public TempoChangeMidiStreamEvent(uint deltaTime, int tempo)
		{
			this._deltaTime = deltaTime;
			this._tempo = tempo;
		}

		private readonly uint _deltaTime;
		private readonly int _tempo;
		public uint DeltaTime { get { return this._deltaTime; } }
		public int Tempo { get { return this._tempo; } }
	}

	public class MidiStreamEventFactory
	{
		public MidiStreamEventFactory()
		{
			this.events = new List<IMidiStreamEvent>();
		}

		private readonly List<IMidiStreamEvent> events;

		public IEnumerable<IMidiStreamEvent> Events { get { return this.events; } }

		public void NoteOn(uint deltaTime, int channel, int key, int velocity)
		{
			uint u = MidiMessageBuilder.NoteOn(channel, key, velocity);
			var e = new MidiStreamEvent(deltaTime, u);
			this.events.Add(e);
		}

		public void NoteOff(uint deltaTime, int channel, int key, int velocity)
		{
			uint u = MidiMessageBuilder.NoteOff(channel, key, velocity);
			var e = new MidiStreamEvent(deltaTime, u);
			this.events.Add(e);
		}

		public void ProgramChange(uint deltaTime, int channel, int program)
		{
			uint u = MidiMessageBuilder.ProgramChange(channel, program);
			var e = new MidiStreamEvent(deltaTime, u);
			this.events.Add(e);
		}

		public void ControlChange(uint deltaTime, int channel, int controller, int value)
		{
			uint u = MidiMessageBuilder.ControlChange(channel, controller, value);
			var e = new MidiStreamEvent(deltaTime, u);
			this.events.Add(e);
		}

		public void ChannelPressure(uint deltaTime, int channel, int value)
		{
			uint u = MidiMessageBuilder.ChannelPressure(channel, value);
			var e = new MidiStreamEvent(deltaTime, u);
			this.events.Add(e);
		}

		public void PolyphonicKeyPressure(uint deltaTime, int channel, int key, int value)
		{
			uint u = MidiMessageBuilder.PolyphonicKeyPressure(channel, key, value);
			var e = new MidiStreamEvent(deltaTime, u);
			this.events.Add(e);
		}

		public void TempoChange(uint deltaTime, int newTempo)
		{
			var e = new TempoChangeMidiStreamEvent(deltaTime, newTempo);
			this.events.Add(e);
		}
	}
}

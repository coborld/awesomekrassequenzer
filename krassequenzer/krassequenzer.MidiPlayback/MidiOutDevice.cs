using krassequenzer.MidiPlayback.Low;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MidiPlayback
{
	public class MidiOutDevice : IMidiOutDevice, IDisposable
	{
		public MidiOutDevice(MidiOutDeviceInfo info)
		{
			if (info == null) throw new ArgumentNullException("info");
			this._info = info;
		}

		private readonly MidiOutDeviceInfo _info;

		public MidiOutDeviceInfo Info { get { return this._info; } }

		private MidiOutSafeHandle currentHandle;

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
				Debug.WriteLine("Disposing midi out safe handle...");
				this.currentHandle.Dispose();
				Debug.WriteLine("Midi out safe handle disposed.");
				this.currentHandle = null;
			}
		}

		public void Open()
		{
			MidiOutSafeHandle handle;
			Debug.WriteLine("Opening midi device...");
			var result = NativeMethods.midiOutOpen(out handle, this.Info.Id, this.MidiProc, IntPtr.Zero, NativeMethods.CALLBACK_FUNCTION);
			NativeMethods.CheckMidiOutMmsyserr(result);
			Debug.WriteLine("Midi device opened.");
			this.currentHandle = handle;
		}

		public void Play()
		{
			NativeMethods.midiOutShortMsg(this.currentHandle, 0x00553391);
		}

		public void ShortMessage(byte b0, byte b1, byte b2)
		{
			uint u = (uint)b0 | ((uint)b1 << 8) | ((uint)b2 << 16);
			this.ShortMessage(u);
		}

		public void ShortMessage(uint message)
		{
			var result = NativeMethods.midiOutShortMsg(this.currentHandle, message);
			NativeMethods.CheckMidiOutMmsyserr(result);
		}

		private void MidiProc(IntPtr handle, uint msg, IntPtr instance, IntPtr param1, IntPtr param2)
		{
			Debug.WriteLine("midi proc?");
		}
	}
}

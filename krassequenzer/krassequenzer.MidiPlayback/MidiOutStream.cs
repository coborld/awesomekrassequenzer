using ConsoleApplication1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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

		public void Play()
		{
			NativeMethods.midiOutPrepareHeader(this.currentHandle, )
		}

		private void MidiProc(IntPtr handle, uint msg, IntPtr instance, IntPtr param1, IntPtr param2)
		{
			Debug.WriteLine("midi proc?");
		}
	}
}

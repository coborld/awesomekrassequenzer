using Microsoft.Win32.SafeHandles;

namespace ConsoleApplication1
{
	/// <summary>
	/// The MidiSafeHandle represents a handle to a midi device/port.
	/// </summary>
	internal abstract class MidiSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		protected MidiSafeHandle()
			: base(true)
		{
			// Create a SafeHandle, informing the base class that this SafeHandle
			// instance "owns" the handle, and therefore SafeHandle should call
			// our ReleaseHandle method when the SafeHandle is no longer in use.
			// This method is called only via P/Invoke.
		}
	}

	internal class MidiInSafeHandle : MidiSafeHandle
	{
		private MidiInSafeHandle()
		{
		}

		protected override bool ReleaseHandle()
		{
			int mmsyserr = NativeMethods.midiInClose(this.handle);
			return mmsyserr == NativeMethods.MMSYSERR_NOERROR;
		}
	}

	internal class MidiOutSafeHandle : MidiSafeHandle
	{
		private MidiOutSafeHandle()
		{
		}

		protected override bool ReleaseHandle()
		{
			NativeMethods.midiOutReset(this.handle);
			int result = NativeMethods.midiOutClose(this.handle);
			return result == NativeMethods.MMSYSERR_NOERROR;
		}
	}

	/// <summary>
	/// SafeHandle implementation for a MidiOutStreamPort.
	/// </summary>
	internal class MidiOutStreamSafeHandle : MidiSafeHandle
	{
		/// <summary>
		/// Closes the port handle (with retry).
		/// </summary>
		/// <returns>Returns true when successful.</returns>
		protected override bool ReleaseHandle()
		{
			int result = NativeMethods.midiStreamClose(this.handle);
			return result == NativeMethods.MMSYSERR_NOERROR;
		}
	}
}

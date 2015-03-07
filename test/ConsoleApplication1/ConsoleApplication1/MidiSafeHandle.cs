using Microsoft.Win32.SafeHandles;

namespace ConsoleApplication1
{
	/// <summary>
	/// The MidiSafeHandle represents a handle to a midi device/port.
	/// </summary>
	public abstract class MidiSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		/// <summary>
		/// Constructs a new instance that owns the handle.
		/// </summary>
		protected MidiSafeHandle()
			: base(true)
		{
		}
	}

	/// <summary>
	/// SafeHandle implementation for a MidiInPort.
	/// </summary>
	internal class MidiInSafeHandle : MidiSafeHandle
	{
		/// <summary>
		/// Closes the port handle.
		/// </summary>
		/// <returns>Returns true when successful.</returns>
		protected override bool ReleaseHandle()
		{
			int result = NativeMethods.midiInClose(handle);

			return result == NativeMethods.MMSYSERR_NOERROR;
		}
	}

	/// <summary>
	/// SafeHandle implementation for a MidiOutPort.
	/// </summary>
	internal class MidiOutSafeHandle : MidiSafeHandle
	{
		/// <summary>
		/// Closes the port handle (with retry).
		/// </summary>
		/// <returns>Returns true when successful.</returns>
		protected override bool ReleaseHandle()
		{
			int result = NativeMethods.midiOutClose(handle);

			if (result == NativeMethods.MIDIERR_STILLPLAYING)
			{
				result = NativeMethods.midiOutReset(this);

				result = NativeMethods.midiOutClose(handle);
			}

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
			int result = NativeMethods.midiStreamClose(handle);

			if (result == NativeMethods.MIDIERR_STILLPLAYING)
			{
				result = NativeMethods.midiOutReset(this);

				result = NativeMethods.midiStreamClose(handle);
			}

			return result == NativeMethods.MMSYSERR_NOERROR;
		}
	}
}

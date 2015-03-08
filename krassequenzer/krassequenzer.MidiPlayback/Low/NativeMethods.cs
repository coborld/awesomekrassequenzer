using krassequenzer.MidiPlayback;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace krassequenzer.MidiPlayback.Low
{

	/// <summary>
	/// The NativeMethods static class implements the P-invoke calls to the Win32
	/// multi-media midi API (midiXxxxx).
	/// </summary>
	internal static class NativeMethods
	{
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern MemorySafeHandle LocalAlloc(uint flags, int size);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr LocalFree(IntPtr handle);

		/// <summary>
		/// Represents the method that handles messages from the midi driver.
		/// </summary>
		/// <param name="handle">Port handle.</param>
		/// <param name="msg">Type of message.</param>
		/// <param name="instance">The instance handle of the port object.</param>
		/// <param name="param1">First parameter.</param>
		/// <param name="param2">Second parameter.</param>
		public delegate void MidiProc(IntPtr handle, uint msg, IntPtr instance, IntPtr param1, IntPtr param2);

		/// <summary>
		/// Retrieves an error text for an Midi In Port <paramref name="errCode"/>.
		/// </summary>
		/// <param name="errCode">The error code.</param>
		/// <param name="errMsg">Receives the error text.</param>
		/// <param name="sizeOfErrMsg">The length of the initialized <paramref name="errMsg"/> object.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll", CharSet = CharSet.Unicode)]
		public static extern int midiInGetErrorText(int errCode, StringBuilder errMsg, int sizeOfErrMsg);

		/// <summary>
		/// Opens a midi in port.
		/// </summary>
		/// <param name="handle">Receives the handle of the midi in port.</param>
		/// <param name="deviceId">A logical port identifier.</param>
		/// <param name="proc">The callback midi procedure.</param>
		/// <param name="instance">The instance handle of the midi in port object.</param>
		/// <param name="flags">Option flags.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiInOpen(out MidiInSafeHandle handle, uint deviceId, MidiProc proc, IntPtr instance, uint flags);

		/// <summary>
		/// Closes the midi in port.
		/// </summary>
		/// <param name="handle">The initialized port handle.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiInClose(IntPtr handle);

		/// <summary>
		/// Starts receiving midi data on a midi in port.
		/// </summary>
		/// <param name="handle">The midi in port handle.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiInStart(MidiSafeHandle handle);

		/// <summary>
		/// Stops receiving data on a midi in port.
		/// </summary>
		/// <param name="handle">The midi in port handle.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiInStop(MidiSafeHandle handle);

		/// <summary>
		/// Resets a midi in port, returning added buffers to the client.
		/// </summary>
		/// <param name="handle">The midi in port handle.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiInReset(MidiSafeHandle handle);

		/// <summary>
		/// Prepares buffer memory for use by the midi in port.
		/// </summary>
		/// <param name="handle">The midi in port handle.</param>
		/// <param name="header">Pointer to the header information for the buffer.</param>
		/// <param name="sizeOfmidiHeader">The size of the midi header structure.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiInPrepareHeader(MidiSafeHandle handle, IntPtr header, uint sizeOfmidiHeader);

		/// <summary>
		/// Undoes the preparation of the buffer memory.
		/// </summary>
		/// <param name="handle">The handle of the midi in port.</param>
		/// <param name="header">Pointer to the header information for the buffer.</param>
		/// <param name="sizeOfmidiHeader">The size of the midi header structure.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiInUnprepareHeader(MidiSafeHandle handle, IntPtr header, uint sizeOfmidiHeader);

		/// <summary>
		/// Adds a buffer to the midi in port for storing received midi data.
		/// </summary>
		/// <param name="handle">The handle of the midi in port.</param>
		/// <param name="header">Pointer to the header information for the buffer.</param>
		/// <param name="sizeOfmidiHeader">The size of the midi header structure.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiInAddBuffer(MidiSafeHandle handle, IntPtr header, uint sizeOfmidiHeader);

		/// <summary>
		/// Retrieves port capabilities information for the midi in port.
		/// </summary>
		/// <param name="deviceId">A logical port identifier.</param>
		/// <param name="caps">A reference to the structure receiving the information.</param>
		/// <param name="sizeOfmidiInCaps">The size of that structure.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiInGetDevCaps(IntPtr deviceId, ref MidiInCaps caps, uint sizeOfmidiInCaps);

		/// <summary>
		/// Retrieves the number of midi in ports.
		/// </summary>
		/// <returns>Returns the number.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiInGetNumDevs();

		/// <summary>
		/// Connects two midi ports.
		/// </summary>
		/// <param name="inHandle">The handle of a midi port.</param>
		/// <param name="outHandle">The handle of a midi port.</param>
		/// <param name="reserved">Not used.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiConnect(MidiSafeHandle inHandle, MidiSafeHandle outHandle, IntPtr reserved);

		/// <summary>
		/// Disconnects two midi ports.
		/// </summary>
		/// <param name="inHandle">The handle of a midi port.</param>
		/// <param name="outHandle">The handle of a midi port.</param>
		/// <param name="reserved">Not used.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiDisconnect(MidiSafeHandle inHandle, MidiSafeHandle outHandle, IntPtr reserved);

		/// <summary>
		/// Opens a midi out port.
		/// </summary>
		/// <param name="handle">Receives the midi out port handle.</param>
		/// <param name="deviceId">The logical port identifier.</param>
		/// <param name="proc">The midi callback procedure.</param>
		/// <param name="instance">An instance handle to the midi out port object.</param>
		/// <param name="flags">Option flags.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiOutOpen(out MidiOutSafeHandle handle, uint deviceId, MidiProc proc, IntPtr instance, uint flags);

		/// <summary>
		/// Closes a midi out port.
		/// </summary>
		/// <param name="handle">The handle to a midi out port.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiOutClose(IntPtr handle);

		/// <summary>
		/// Resets a midi out port, returning any processing buffers to the client.
		/// </summary>
		/// <param name="handle">The handle to a midi out port.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiOutReset(IntPtr handle);

		/// <summary>
		/// Outputs a short midi message over the midi out port.
		/// </summary>
		/// <param name="handle">The handle to a midi out port.</param>
		/// <param name="message">The short midi message.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiOutShortMsg(MidiSafeHandle handle, uint message);

		/// <summary>
		/// Prepares buffer memory for use by the midi out port.
		/// </summary>
		/// <param name="handle">The midi out port handle.</param>
		/// <param name="header">Pointer to the header information for the buffer.</param>
		/// <param name="sizeOfmidiHeader">The size of the midi header structure.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiOutPrepareHeader(MidiSafeHandle handle, MemorySafeHandle header, uint sizeOfmidiHeader);

		/// <summary>
		/// Undoes the preparation of the buffer memory.
		/// </summary>
		/// <param name="handle">The handle of the midi out port.</param>
		/// <param name="header">Pointer to the header information for the buffer.</param>
		/// <param name="sizeOfmidiHeader">The size of the midi header structure.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiOutUnprepareHeader(MidiSafeHandle handle, MemorySafeHandle header, uint sizeOfmidiHeader);

		/// <summary>
		/// Outputs a long midi message to the midi out port.
		/// </summary>
		/// <param name="handle">The handle of the midi out port.</param>
		/// <param name="header">Pointer to the header information for the buffer.</param>
		/// <param name="sizeOfmidiHeader">The size of the midi header structure.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiOutLongMsg(MidiSafeHandle handle, IntPtr header, uint sizeOfmidiHeader);

		/// <summary>
		/// Retrieves an error text for an midi out port <paramref name="errCode"/>.
		/// </summary>
		/// <param name="errCode">The error code.</param>
		/// <param name="errMsg">Receives the error text.</param>
		/// <param name="sizeOfErrMsg">The length of the initialized <paramref name="errMsg"/> object.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll", CharSet = CharSet.Unicode)]
		public static extern int midiOutGetErrorText(int errCode, StringBuilder errMsg, int sizeOfErrMsg);

		/// <summary>
		/// Retrieves the device capabilities for a specific midi out device.
		/// </summary>
		/// <param name="deviceId">The logical identification of the device.</param>
		/// <param name="caps">A structure that will receive the information.</param>
		/// <param name="sizeOfmidiOutCaps">The size of the structure that will receive the information.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiOutGetDevCaps(IntPtr deviceId, ref MidiOutCaps caps, uint sizeOfmidiOutCaps);

		/// <summary>
		/// Retrieves the number of midi out devices.
		/// </summary>
		/// <returns>Returns the number.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiOutGetNumDevs();

		/// <summary>
		/// Closes a midi stream port.
		/// </summary>
		/// <param name="handle">The midi stream port handle.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiStreamClose(IntPtr handle);

		/// <summary>
		/// Opens a midi stream port (out).
		/// </summary>
		/// <param name="handle">Receives the handle of the port when its open.</param>
		/// <param name="deviceId">A reference to the logical identifier of the port.</param>
		/// <param name="cMidi">Not used. 1.</param>
		/// <param name="proc">The midi callback procedure.</param>
		/// <param name="instance">An instance handle of the midi stream object.</param>
		/// <param name="flags">Option flags.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiStreamOpen(out MidiOutStreamSafeHandle handle, ref uint deviceId, uint cMidi, MidiProc proc, IntPtr instance, uint flags);

		/// <summary>
		/// Outputs a buffer to the stream port.
		/// </summary>
		/// <param name="handle">The handle of the midi stream port.</param>
		/// <param name="header">Pointer to the header information for the buffer.</param>
		/// <param name="sizeOfmidiHeader">The size of the midi header structure.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiStreamOut(MidiSafeHandle handle, MemorySafeHandle header, uint sizeOfmidiHeader);

		/// <summary>
		/// Pauses playback of the midi stream port.
		/// </summary>
		/// <param name="handle">The handle of the midi stream port.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiStreamPause(MidiSafeHandle handle);

		/// <summary>
		/// Starts (or restarts) playback on the stream port.
		/// </summary>
		/// <param name="handle">The handle of the midi stream port.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiStreamRestart(MidiSafeHandle handle);

		/// <summary>
		/// Stops the playback of the midi stream port.
		/// </summary>
		/// <param name="handle">The handle of the midi stream port.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiStreamStop(MidiSafeHandle handle);

		/// <summary>
		/// Retrieves the time position of the playback of a stream port.
		/// </summary>
		/// <param name="handle">The handle of a stream port.</param>
		/// <param name="time">A reference to the structure that will receive the time.</param>
		/// <param name="sizeOfMmTime">The size of that structure.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiStreamPosition(MidiSafeHandle handle, ref MmTime time, uint sizeOfMmTime);

		/// <summary>
		/// Gets or sets a property on the midi stream port.
		/// </summary>
		/// <param name="handle">The handle of the midi stream port.</param>
		/// <param name="prop">A reference to the property information.</param>
		/// <param name="flags">Option flags.</param>
		/// <returns>Returns zero when successful.</returns>
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Justification = "Performance")]
		[DllImport("winmm.dll")]
		public static extern int midiStreamProperty(MidiSafeHandle handle, ref MidiOutStreamPortProperty prop, uint flags);

		/// <summary>Max length of an error text.</summary>
		public const int MAXERRORLENGTH = 256;

		/// <summary>Error code that is not an error.</summary>
		public const int MMSYSERR_NOERROR = 0;

		public const int MMSYSERR_BADDEVICEID = 2;

		/// <summary>Error indicating the buffer is used for playing.</summary>
		public const int MIDIERR_STILLPLAYING = 65;

		/// <summary>Midi open flag for using a callback procedure.</summary>
		public const uint CALLBACK_FUNCTION = 0x30000;

		/// <summary>Midi open flag for status callbacks.</summary>
		public const uint MIDI_IO_STATUS = 32;

		/// <summary>A midi callback message type for port open.</summary>
		public const uint MIM_OPEN = 0x3C1;

		/// <summary>A midi callback message type for port close.</summary>
		public const uint MIM_CLOSE = 0x3C2;

		/// <summary>A midi callback message type for short data.</summary>
		public const uint MIM_DATA = 0x3C3;

		/// <summary>A midi callback message type for long data.</summary>
		public const uint MIM_LONGDATA = 0x3C4;

		/// <summary>A midi callback message type for short error.</summary>
		public const uint MIM_ERROR = 0x3C5;

		/// <summary>A midi callback message type for long error.</summary>
		public const uint MIM_LONGERROR = 0x3C6;

		/// <summary>A midi callback message type for port open.</summary>
		public const uint MOM_OPEN = 0x3C7;

		/// <summary>A midi callback message type for port close.</summary>
		public const uint MOM_CLOSE = 0x3C8;

		/// <summary>A midi callback message type for buffer done.</summary>
		public const uint MOM_DONE = 0x3C9;

		/// <summary>A midi callback message type for positional callback notification.</summary>
		public const uint MOM_POSITIONCB = 0x3CA;

		/// <summary>A midi callback message type for overrun data.</summary>
		public const uint MIM_MOREDATA = 0x3CC;

		/// <summary>A midi buffer header flag indicating processing is done.</summary>
		public const uint MHDR_DONE = 1;

		/// <summary>A midi buffer header flag indicating the buffer is prepare by the port.</summary>
		public const uint MHDR_PREPARED = 2;

		/// <summary>A midi buffer header flag indicating the buffer is in the queue.</summary>
		public const uint MHDR_INQUEUE = 4;

		/// <summary>A midi buffer header flag indicating the buffer is a stream.</summary>
		public const uint MHDR_ISSTRM = 8;

		////public const uint MM_STREAM_OPEN = 0x3D4;
		////public const uint MM_STREAM_CLOSE = 0x3D5;
		////public const uint MM_STREAM_DONE = 0x3D6;
		////public const uint MM_STREAM_ERROR = 0x3D7;

		/// <summary>Midi stream port property option flags.</summary>
		public const uint MIDIPROP_SET = 0x80000000;

		/// <summary>Midi stream port property option flags.</summary>
		public const uint MIDIPROP_GET = 0x40000000;

		/// <summary>Midi stream port property option flags.</summary>
		public const uint MIDIPROP_TIMEDIV = 0x00000001;

		/// <summary>Midi stream port property option flags.</summary>
		public const uint MIDIPROP_TEMPO = 0x00000002;

		public static void CheckMidiOutMmsyserr(int mmsyserr)
		{
			if (mmsyserr != MMSYSERR_NOERROR)
			{
				var sb = new StringBuilder(MAXERRORLENGTH);
				var errorResult = midiOutGetErrorText(mmsyserr, sb, sb.Capacity);
				if (errorResult != MMSYSERR_NOERROR)
				{
					throw new MidiException("An error occurred and there was another error while attempting to retrieve the error message.", mmsyserr);
				}
				throw new MidiException(sb.ToString(), mmsyserr);
			}
		}
	}

	internal class InteropStructSizes
	{
		/// <summary>
		/// Returns the native size of a <paramref name="type"/>.
		/// </summary>
		/// <param name="type">Must not be null.</param>
		/// <returns>Returns a positive integer.</returns>
		private static int SizeOf(Type type)
		{
			return Marshal.SizeOf(type);
		}

#if false
		/// <summary>
		/// Free's the unmanaged memory allocated by <see cref="M:Alloc"/>.
		/// </summary>
		/// <param name="memory">Memory pointer. Must not be IntPtr.Zero.</param>
		public static void Free(IntPtr memory)
		{
			Contract.Requires(memory != IntPtr.Zero);

			Marshal.FreeHGlobal(memory);
		}

		/// <summary>
		/// Allocates unmanaged memory of <paramref name="size"/> bytes.
		/// </summary>
		/// <param name="size">A positive integer.</param>
		/// <returns>Returns a pointer to the memory.</returns>
		public static IntPtr Alloc(int size)
		{
			Contract.Requires(size > 0);

			return Marshal.AllocHGlobal(size);
		}
#endif

		/// <summary>The native size of <see cref="MidiHeader"/>.</summary>
		public static readonly int SizeOfMidiHeader = SizeOf(typeof(MidiHeader));

		/// <summary>The native size of <see cref="MidiInCaps"/>.</summary>
		public static readonly int SizeOfMidiInCaps = SizeOf(typeof(MidiInCaps));

		/// <summary>The native size of <see cref="MidiOutCaps"/>.</summary>
		public static readonly int SizeOfMidiOutCaps = SizeOf(typeof(MidiOutCaps));

		/// <summary>The native size of <see cref="MmTime"/>.</summary>
		public static readonly int SizeOfMmTime = SizeOf(typeof(MmTime));

		/// <summary>The native size of <see cref="MidiOutStreamPortProperty"/>.</summary>
		public static readonly int SizeOfMidiStreamOutPortProperty = SizeOf(typeof(MidiOutStreamPortProperty));
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct MidiInCaps
	{
		/// <summary>Midi in caps property.</summary>
		public ushort Mid;

		/// <summary>Midi in caps property.</summary>
		public ushort Pid;

		/// <summary>Midi in caps property.</summary>
		public uint DriverVersion;

		/// <summary>Midi in caps property.</summary>
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string Name;

		/// <summary>Midi in caps property.</summary>
		public uint Support;
	}

	/// <summary>
	/// typedef struct {
	///   WORD      wMid;
	///   WORD      wPid;
	///   MMVERSION vDriverVersion;
	///   TCHAR     szPname[MAXPNAMELEN];
	///   WORD      wTechnology;
	///   WORD      wVoices;
	///   WORD      wNotes;
	///   WORD      wChannelMask;
	///   DWORD     dwSupport;
	/// } MIDIOUTCAPS;
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct MidiOutCaps
	{
		public ushort wMid;
		public ushort wPid;
		public Mmversion vDriverVersion;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string szPname;
		public ushort wTechnology;
		public ushort wVoices;
		public ushort wNotes;
		public ushort wChannelMask;
		public uint dwSupport;
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct Mmversion
	{
		public uint Version;
	}

	/// <summary>
	/// Internal time structure passed to <see cref="NativeMethods"/>.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
	internal struct MmTime
	{
		/// <summary>Unmanaged MmTime property.</summary>
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Needed for marshalling.")]
		[FieldOffset(0)]
		public uint Type;

		/// <summary>Unmanaged MmTime property.</summary>
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Needed for marshalling.")]
		[FieldOffset(4)]
		public uint MilliSeconds;

		/// <summary>Unmanaged MmTime property.</summary>
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Needed for marshalling.")]
		[FieldOffset(4)]
		public uint Sample;

		/// <summary>Unmanaged MmTime property.</summary>
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Needed for marshalling.")]
		[FieldOffset(4)]
		public uint ByteCount;

		/// <summary>Unmanaged MmTime property.</summary>
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Needed for marshalling.")]
		[FieldOffset(4)]
		public uint Ticks;

		/// <summary>Unmanaged MmTime property.</summary>
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Needed for marshalling.")]
		[FieldOffset(4)]
		public uint MidiSongPtrPos;

		/// <summary>Unmanaged MmTime property.</summary>
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Needed for marshalling.")]
		[FieldOffset(4)]
		public byte SmpteHour;

		/// <summary>Unmanaged MmTime property.</summary>
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Needed for marshalling.")]
		[FieldOffset(5)]
		public byte SmpteMin;

		/// <summary>Unmanaged MmTime property.</summary>
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Needed for marshalling.")]
		[FieldOffset(6)]
		public byte SmpteSec;

		/// <summary>Unmanaged MmTime property.</summary>
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Needed for marshalling.")]
		[FieldOffset(7)]
		public byte SmpteFrame;

		/// <summary>Unmanaged MmTime property.</summary>
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Needed for marshalling.")]
		[FieldOffset(8)]
		public byte SmpteFps;

		/// <summary>Unmanaged MmTime property.</summary>
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Needed for marshalling.")]
		[FieldOffset(9)]
		public byte SmpteDummy;

		/// <summary>Unmanaged MmTime property.</summary>
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Needed for marshalling.")]
		[FieldOffset(10)]
		public byte SmptePad0;

		/// <summary>Unmanaged MmTime property.</summary>
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Needed for marshalling.")]
		[FieldOffset(11)]
		public byte SmptePad1;
	}

	/// <summary>
	/// The unmanaged structure for Stream Port properties.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct MidiOutStreamPortProperty
	{
		/// <summary>
		/// Constructs a new instance with a <paramref name="value"/>.
		/// </summary>
		/// <param name="value">Can be zero.</param>
		public MidiOutStreamPortProperty(uint value)
		{
			this.StructSize = (uint)InteropStructSizes.SizeOfMidiStreamOutPortProperty;
			this.PropertyValue = value;
		}

		/// <summary>Unmanaged structure property.</summary>
		public uint StructSize;

		/// <summary>Unmanaged structure property.</summary>
		public uint PropertyValue;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct MidiHeader
	{
		public IntPtr Data;
		public uint BufferLength;
		public uint BytesRecorded;
		public IntPtr UserData;
		public uint Flags;
		public IntPtr Next;
		public uint Reserved;
		public uint Offset;
		//[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		//public IntPtr[] Reserved2;

		public IntPtr Reserved0;
		public IntPtr Reserved1;
		public IntPtr Reserved2;
		public IntPtr Reserved3;
		public IntPtr Reserved4;
		public IntPtr Reserved5;
		public IntPtr Reserved6;
		public IntPtr Reserved7;
	}
}
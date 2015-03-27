using krassequenzer.MidiPlayback.Low;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MidiPlayback
{
	/// <summary>
	/// Provides information about available MIDI devices in the system.
	/// </summary>
	public class MidiSystemInfo
	{
		private MidiSystemInfo(List<MidiOutDeviceInfo> outDeviceInfo)
		{
			this._outDeviceInfo = outDeviceInfo.AsReadOnly();
		}

		private readonly IList<MidiOutDeviceInfo> _outDeviceInfo;
		/// <summary>
		/// Gets a list of MIDI output devices.
		/// </summary>
		public IList<MidiOutDeviceInfo> OutDeviceInfo { get { return this._outDeviceInfo; } }

		/// <summary>
		/// Queries the available MIDI devices in the system.
		/// </summary>
		public static MidiSystemInfo Query()
		{
			var outCaps = GetOutDevices();

			var info = new MidiSystemInfo(outCaps);
			return info;
		}

		private static List<MidiOutDeviceInfo> GetOutDevices()
		{
			var list = new List<MidiOutDeviceInfo>();

			var numDevices = NativeMethods.midiOutGetNumDevs();
			for (int i = 0; i < numDevices; ++i)
			{
				MidiOutDeviceInfo info;
				var ex = QueryOutDeviceInternal(i, out info);
				var midiException = ex as MidiException;
				if (midiException != null && midiException.Mmsyserr == NativeMethods.MMSYSERR_BADDEVICEID)
				{
					// looks like someone disconnected a device after the midiOutGetNumDevs call.
					continue;
				}
				list.Add(info);
			}

			return list;
		}

		private static Exception QueryOutDeviceInternal(int deviceId, out MidiOutDeviceInfo info)
		{
			if (deviceId < 0) throw new ArgumentOutOfRangeException("deviceId");

			var handle = new IntPtr(deviceId);
			var caps = new MidiOutCaps();

			var result = NativeMethods.midiOutGetDevCaps(handle, ref caps, (uint)InteropStructSizes.SizeOfMidiOutCaps);
			if (result != NativeMethods.MMSYSERR_NOERROR)
			{
				info = null;
				return new MidiException(GetMmsyserrString(result), result);
			}

			info = new MidiOutDeviceInfo((uint)deviceId, caps);
			return null;
		}

		/// <summary>
		/// Gets a <see cref="MidiOutDeviceInfo"/> instance associated with the specified <paramref name="deviceId"/>.
		/// This function may throw if the device is not available.
		/// </summary>
		public static MidiOutDeviceInfo QueryOutDevice(int deviceId)
		{
			MidiOutDeviceInfo info;
			var ex = QueryOutDeviceInternal(deviceId, out info);
			if (ex != null)
			{
				throw new Exception(ex.Message, ex);
			}
			return info;
		}

		private static void ThrowMmsyserr(int mmsyserr)
		{
			throw new IOException(GetMmsyserrString(mmsyserr));
		}

		private static string GetMmsyserrString(int mmsyserr)
		{
			StringBuilder errorTextBuilder = new StringBuilder(NativeMethods.MAXERRORLENGTH);
			var retval = NativeMethods.midiOutGetErrorText(mmsyserr, errorTextBuilder, errorTextBuilder.Capacity);
			return errorTextBuilder.ToString();
		}
	}

	/// <summary>
	/// Represents a MIDI output device.
	/// </summary>
	public class MidiOutDeviceInfo
	{
		/// <summary>
		/// Initializes a new instance.
		/// </summary>
		/// <param name="id">The device ID.</param>
		/// <param name="caps">The <see cref="MidiOutCaps"/> instance containing
		/// information about the device.</param>
		internal MidiOutDeviceInfo(uint id, MidiOutCaps caps)
		{
			this._id = id;
			this.caps = caps;
		}

		private readonly uint _id;
		/// <summary>
		/// This must be hidden because it is a mutable struct.
		/// </summary>
		private readonly MidiOutCaps caps;

		/// <summary>
		/// Gets the device ID.
		/// </summary>
		public uint Id { get { return this._id; } }
		/// <summary>
		/// Gets the manufacturer identifier of the device driver for the MIDI output device.
		/// </summary>
		public int ManufacturerId { get { return this.caps.wMid; } }
		/// <summary>
		/// Gets the product identifier of the MIDI output device.
		/// </summary>
		public int ProductId { get { return this.caps.wPid; } }
		/// <summary>
		/// Version number of the device driver for the MIDI output device.
		/// The high-order byte is the major version number, and the low-order byte
		/// is the minor version number.
		/// </summary>
		public uint DriverVersion { get { return this.caps.vDriverVersion.Version; } }
		/// <summary>
		/// Gets the product name.
		/// </summary>
		public string Name { get { return this.caps.szPname; } }
		/// <summary>
		/// Gets the type of the MIDI output device.
		/// </summary>
		public int Technology { get { return this.caps.wTechnology; } }
		/// <summary>
		/// Number of voices supported by an internal synthesizer device.
		/// If the device is a port, this member is not meaningful and is set to 0.
		/// </summary>
		public int Voices { get { return this.caps.wVoices; } }
		/// <summary>
		/// Maximum number of simultaneous notes that can be played by an internal
		/// synthesizer device. If the device is a port, this member is not meaningful
		/// and is set to 0.
		/// </summary>
		public int Notes { get { return this.caps.wNotes; } }
		/// <summary>
		/// Channels that an internal synthesizer device responds to, where the least
		/// significant bit refers to channel 0 and the most significant bit to channel 15.
		/// Port devices that transmit on all channels set this member to 0xFFFF.
		/// </summary>
		public uint ChannelMask { get { return this.caps.wChannelMask; } }
		/// <summary>
		/// Optional functionality supported by the device.
		/// </summary>
		public uint Support { get { return this.caps.dwSupport; } }
	}
}

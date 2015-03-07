using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
	class MidiSystemInfo
	{
		private MidiSystemInfo(List<MidiOutDeviceInfo> outDeviceInfo)
		{
			this._outDeviceInfo = outDeviceInfo.AsReadOnly();
		}

		private readonly IList<MidiOutDeviceInfo> _outDeviceInfo;
		public IList<MidiOutDeviceInfo> OutDeviceInfo { get { return this._outDeviceInfo; } }

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
				var handle = new IntPtr(i);
				var caps = new MidiOutCaps();
				
				var result = NativeMethods.midiOutGetDevCaps(handle, ref caps, (uint)MemoryUtil.SizeOfMidiOutCaps);
				if (result == NativeMethods.MMSYSERR_BADDEVICEID)
				{
					// looks like someone disconnected a device after the midiOutGetNumDevs call.
					continue;
				}
				if (result != NativeMethods.MMSYSERR_NOERROR)
				{
					ThrowMmsyserr(result);
				}

				var info = new MidiOutDeviceInfo(i, caps);
				list.Add(info);
			}

			return list;
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

	class MidiOutDeviceInfo
	{
		public MidiOutDeviceInfo(int id, MidiOutCaps caps)
		{
			this._id = id;
			this._caps = caps;
		}

		private readonly int _id;
		private readonly MidiOutCaps _caps;

		public int Id { get { return this._id; } }
		public MidiOutCaps Caps { get { return this._caps; } }
	}
}

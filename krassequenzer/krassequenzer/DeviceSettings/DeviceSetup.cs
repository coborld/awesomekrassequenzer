using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.DeviceSettings
{
	/// <summary>
	/// Represents the current device setup.
	/// Instances of this class are immutable.
	/// </summary>
	public class DeviceSetup
	{
		/// <summary>
		/// Initializes a new instance.
		/// </summary>
		public DeviceSetup(int midiOutputDeviceId)
		{
			this._midiOutputDeviceId = midiOutputDeviceId;
		}

		private readonly int _midiOutputDeviceId;
		/// <summary>
		/// Gets the selected MIDI output device ID.
		/// </summary>
		public int MidiOutputDeviceId { get { return this._midiOutputDeviceId; } }
	}
}

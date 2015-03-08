using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MidiPlayback.Low
{
	class MemorySafeHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		private MemorySafeHandle()
			: base(true)
		{
		}

		protected override bool ReleaseHandle()
		{
			var ret = NativeMethods.LocalFree(this.handle);
			return ret == IntPtr.Zero;
		}
	}
}

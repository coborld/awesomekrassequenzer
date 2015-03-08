using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MidiPlayback.Low
{
	static class UnmanagedMemory
	{
		public static MemorySafeHandle AllocFixed(int bytes)
		{
			return NativeMethods.LocalAlloc(0, bytes);
		}
	}
}

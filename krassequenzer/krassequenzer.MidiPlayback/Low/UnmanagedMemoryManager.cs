using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace krassequenzer.MidiPlayback.Low
{
	/// <summary>
	/// Managed the allocation and freeing of an unmanaged block of memory.
	/// </summary>
	sealed class UnmanagedMemoryManager : IDisposable
	{
		// We use LocalAlloc and LocalFree instead of HeapAlloc and HeapFree
		// because that is what Marshal.AllocHGlobal and Marshal.AllocHFree
		// use. So it can't be that bad.

		[DllImport("kernel32.dll")]
		private static extern MemorySafeHandle LocalAlloc(uint flags, int cb);
		[DllImport("kernel32.dll")]
		private static extern IntPtr LocalFree(IntPtr memory);

		private class MemorySafeHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			private MemorySafeHandle()
				: base(true)
			{
			}

			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			protected override bool ReleaseHandle()
			{
				var ret = UnmanagedMemoryManager.LocalFree(this.handle);
				return ret == IntPtr.Zero;
			}
		}

		/// <summary>
		/// Allocates memory.
		/// The memory is only valid during the lifetime of this instance. If this
		/// instance is disposed, the memory is freed.
		/// </summary>
		/// <param name="cb">Number of bytes to allocate</param>
		public IntPtr Alloc(int cb)
		{
			var memory = LocalAlloc(0, cb);
			if (memory.IsInvalid)
			{
				throw new OutOfMemoryException("Memory allocation failed (out of memory).");
			}
			var handle = memory.DangerousGetHandle();
			this.memoryList.Add(memory);
			return handle;
		}

		/// <summary>
		/// Frees the memory associated with this instance.
		/// You must make sure that the memory is not used anymore
		/// before calling this method.
		/// </summary>
		public void Dispose()
		{
			foreach (var memory in this.memoryList)
			{
				memory.Dispose();
			}
			memoryList.Clear();
		}

		private readonly List<MemorySafeHandle> memoryList = new List<MemorySafeHandle>();
	}
}

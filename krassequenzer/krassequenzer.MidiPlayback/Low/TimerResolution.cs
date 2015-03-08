using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace krassequenzer.MidiPlayback.Low
{
	/// <summary>
	/// Allows the user to manipulate the system timer.
	/// During the lifetime of an instance, the timer frequency can be set
	/// to a specified value.
	/// The actual timer frequency used by the system may be faster than the
	/// specified value, depending on what other applications in the system
	/// have requested.
	/// </summary>
	public sealed class TimerResolution : IDisposable
	{
		private TimerResolution(int resolution_ms)
		{
			this.resolution_ms = resolution_ms;
		}

		private readonly int resolution_ms;
		private bool isDisposed;

		/// <summary>
		/// Switches the system timer frequency so that the specified
		/// resolution is at least met.
		/// </summary>
		/// <param name="resolution_ms">The minimum time that is required
		/// by your application. The actual resolution that is set may be
		/// lower than this.
		/// The minimum timer resolution is 1 ms.</param>
		public static TimerResolution Install(int resolution_ms)
		{
			if (resolution_ms <= 0)
			{
				throw new ArgumentOutOfRangeException("resolution_ms", "Resolution must be positive.");
			}

			var result = timeBeginPeriod(resolution_ms);

			if (result != TIMERR_NOERROR)
			{
				throw new Exception("Failed to set timer resolution (" + result + ").");
			}

			return new TimerResolution(resolution_ms);
		}

		~TimerResolution()
		{
			this.Dispose(false);
		}

		/// <summary>
		/// Disposes of this instance and resets the timer frequency change
		/// it has imposed on the system.
		/// </summary>
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (this.isDisposed)
			{
				return;
			}
			timeEndPeriod(this.resolution_ms);
			this.isDisposed = true;
		}

		private const int TIMERR_NOERROR = 0;

		[DllImport("winmm.dll")]
		private static extern int timeBeginPeriod(int period_ms);

		[DllImport("winmm.dll")]
		private static extern int timeEndPeriod(int period_ms);
	}
}

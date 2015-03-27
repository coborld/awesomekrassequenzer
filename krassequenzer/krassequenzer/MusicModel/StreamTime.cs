using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using krassequenzer.Stuff;

namespace krassequenzer.MusicModel
{
	/// <summary>
	/// Represents a musical time value, defined by ticks.
	/// </summary>
	public struct StreamTime
	{
		// this is so that 16th notes are evenly divisble by
		// 2, 3, 4, 5, 6, 8, 10
		// so we can construct 2, 3, 5 and 6-tuplets prefectly.
		/// <summary>
		/// Gets the number of ticks that make up one quarter note.
		/// </summary>
		public const int TicksPerQuarter = 4 * 120;

		public static StreamTime Zero = new StreamTime(0);

		/// <summary>
		/// Initializes a new instance.
		/// </summary>
		/// <param name="ticks">The number of ticks since the start of the
		/// track.</param>
		public StreamTime(long ticks)
		{
			if (ticks < 0) throw new ArgumentOutOfRangeException("ticks");
			this._ticks = ticks;
		}

		private readonly long _ticks;

		/// <summary>
		/// Gets the number of ticks since the start of the track.
		/// </summary>
		public long Ticks { get { return this._ticks; } }

		/// <summary>
		/// Accepts a notes relative length (e.g. 2,4,8 for a half, quarter, eigth note)
		/// and converts it to an instance of MusicalTime.
		/// </summary>
		/// <param name="beatUnit"></param>
		/// <returns></returns>
		public static StreamTime getByRelativeNoteLength(int beatUnit){
			return new StreamTime(4 / beatUnit * TicksPerQuarter);
		}

		public static int Comparison(StreamTime x, StreamTime y)
		{
			return Math.Sign(x.Ticks - y.Ticks);
		}

		public static bool operator >(StreamTime l, StreamTime r)
		{
			return Comparison(l, r) > 0;
		}

		public static bool operator >=(StreamTime l, StreamTime r)
		{
			return Comparison(l, r) >= 0;
		}

		public static bool operator <(StreamTime l, StreamTime r)
		{
			return Comparison(l, r) < 0;
		}

		public static bool operator <=(StreamTime l, StreamTime r)
		{
			return Comparison(l, r) <= 0;
		}

		public static bool operator ==(StreamTime l, StreamTime r)
		{
			return l.Ticks == r.Ticks;
		}

		public static bool operator !=(StreamTime l, StreamTime r)
		{
			return l.Ticks != r.Ticks;
		}

		public static StreamTime operator -(StreamTime l, StreamTime r)
		{
			return new StreamTime(l.Ticks - r.Ticks);
		}

		public static StreamTime operator +(StreamTime l, StreamTime r)
		{
			return new StreamTime(l.Ticks + r.Ticks);
		}

		public override int GetHashCode()
		{
			return this.Ticks.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is StreamTime))
			{
				return false;
			}
			var o = (StreamTime)obj;
			return this.Ticks == o.Ticks;
		}

		/// <summary>
		/// Returns a string representation containing the number of ticks
		/// in this instance.
		/// </summary>
		public override string ToString()
		{
			return this.Ticks.ToStringIv();
		}
	}
}

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
	public struct MusicalTime
	{
		// this is so that 16th notes are evenly divisble by
		// 2, 3, 4, 5, 6, 8, 10
		public const int TicksPerQuarter = 4 * 120;

		public static MusicalTime Zero = new MusicalTime(0);

		public MusicalTime(long ticks)
		{
			if (ticks < 0) throw new ArgumentOutOfRangeException("ticks");
			this._ticks = ticks;
		}

		private readonly long _ticks;

		public long Ticks { get { return this._ticks; } }

		/// <summary>
		/// Accepts a notes relative length (e.g. 2,4,8 for a half, quarter, eigth note)
		/// and converts it to an instance of MusicalTime.
		/// </summary>
		/// <param name="beatUnit"></param>
		/// <returns></returns>
		public static MusicalTime getByBeatUnit(int beatUnit){
			return new MusicalTime(4 / beatUnit * TicksPerQuarter);
		}

		public static int Comparison(MusicalTime x, MusicalTime y)
		{
			return Math.Sign(x.Ticks - y.Ticks);
		}

		public static bool operator >(MusicalTime l, MusicalTime r)
		{
			return Comparison(l, r) > 0;
		}

		public static bool operator >=(MusicalTime l, MusicalTime r)
		{
			return Comparison(l, r) >= 0;
		}

		public static bool operator <(MusicalTime l, MusicalTime r)
		{
			return Comparison(l, r) < 0;
		}

		public static bool operator <=(MusicalTime l, MusicalTime r)
		{
			return Comparison(l, r) <= 0;
		}

		public static bool operator ==(MusicalTime l, MusicalTime r)
		{
			return l.Ticks == r.Ticks;
		}

		public static bool operator !=(MusicalTime l, MusicalTime r)
		{
			return l.Ticks != r.Ticks;
		}

		public static MusicalTime operator -(MusicalTime l, MusicalTime r)
		{
			return new MusicalTime(l.Ticks - r.Ticks);
		}

		public static MusicalTime operator +(MusicalTime l, MusicalTime r)
		{
			return new MusicalTime(l.Ticks + r.Ticks);
		}

		public override int GetHashCode()
		{
			return this.Ticks.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is MusicalTime))
			{
				return false;
			}
			var o = (MusicalTime)obj;
			return this.Ticks == o.Ticks;
		}
	}
}

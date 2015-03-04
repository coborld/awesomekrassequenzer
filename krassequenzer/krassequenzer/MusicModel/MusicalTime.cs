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
		public const int TicksPerQuarter = 128;

		public MusicalTime(long ticks)
		{
			if (ticks < 0) throw new ArgumentOutOfRangeException("ticks");
			this._ticks = ticks;
		}

		private readonly long _ticks;

		public long Ticks { get { return this._ticks; } }

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

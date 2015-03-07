using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.Stuff
{
	internal static class ExtUtil
	{
		public static T NotNull<T>(this T item, string message)
			where T : class
		{
			if (item == null)
			{
				throw new ArgumentNullException(message);
			}
			return item;
		}

		public static bool IsPowerOf2(this int i)
		{
			if (i <= 0) return false;
			while ((i & 1) == 0)
			{
				i >>= 1;
			}
			i >>= 1;
			return i == 0;
		}

		public static string ToStringIv(this int i)
		{
			return i.ToString(System.Globalization.CultureInfo.InvariantCulture);
		}

		public static string ToStringIv(this double d)
		{
			return d.ToString(System.Globalization.CultureInfo.InvariantCulture);
		}

		public static string ToStringIv(this double d, string format)
		{
			return d.ToString(format, System.Globalization.CultureInfo.InvariantCulture);
		}

		public static string ToStringIv(this long l)
		{
			return l.ToString(System.Globalization.CultureInfo.InvariantCulture);
		}
	}
}

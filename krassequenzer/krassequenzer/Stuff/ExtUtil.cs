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

		/// <summary>
		/// Applies <paramref name="selector"/> to <paramref name="obj"/> if
		/// <paramref name="obj"/> is not null.
		/// </summary>
		public static TOut Maybe<TIn, TOut>(this TIn obj, Func<TIn, TOut> selector)
		{
			selector.NotNull("selector");
			if (Object.ReferenceEquals(obj, null))
			{
				return default(TOut);
			}
			return selector(obj);
		}

		/// <summary>
		/// Applies <paramref name="action"/> to <paramref name="obj"/> if
		/// <paramref name="obj"/> is not null.
		/// </summary>
		public static void Maybe<TIn>(this TIn obj, Action<TIn> action)
		{
			action.NotNull("action");
			if (!Object.ReferenceEquals(obj, null))
			{
				action(obj);
			}
		}
	}
}

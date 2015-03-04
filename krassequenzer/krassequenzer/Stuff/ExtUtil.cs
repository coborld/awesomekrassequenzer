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
		{
			if (Object.ReferenceEquals(item, null))
			{
				throw new ArgumentNullException(message);
			}
			return item;
		}
	}
}

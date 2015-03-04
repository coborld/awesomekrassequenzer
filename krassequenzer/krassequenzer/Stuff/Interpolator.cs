using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.Stuff
{
	internal static class Interpolator
	{
		public static double Interp2(double x, double x1, double y1, double x2, double y2)
		{
			var dx = x2 - x1;
			var dy = y2 - y1;
			var px = x - x1;
			var py = px / dx * dy;
			var y =  py + y1;
			return y;
		}
	}
}

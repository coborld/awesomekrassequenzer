using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MusicModel
{
	class Pitch
	{
		public static readonly int c = 60;

		int Absolute { get; set; }

		public String toString()
		{
			return (Absolute % 12).ToString();
		}
	}
}

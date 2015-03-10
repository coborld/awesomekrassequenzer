using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MusicModel
{
	public class Pitch
	{
		public static readonly int c = 60;

		public int Absolute { get; set; }

		public override string ToString()
		{
			return (Absolute % 12).ToString();
		}
	}
}

using krassequenzer.MusicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.ClassicalNotation
{
	public class Modifier
	{
		public static Modifier None = new Modifier(1, 1);
		public static Modifier DottedSingle = new Modifier(3, 2);
		public static Modifier DottedDouble = new Modifier(7, 4);
		public static Modifier DottedTriple = new Modifier(15, 8);

		private readonly int numerator;
		private readonly int denominator;
		public Modifier(int numerator, int denominator)
		{
			this.numerator = numerator;
			this.denominator = denominator;
		}

		public MusicalTime Apply(MusicalTime on)
		{
			return new MusicalTime(on.Ticks / denominator * numerator);
		}

	}
}

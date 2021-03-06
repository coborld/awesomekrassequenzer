﻿using krassequenzer.MusicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MusicModel
{
	public class DurationModifier
	{
		public static DurationModifier None { get { return new DurationModifier(1, 1); } }
		public static DurationModifier DottedSingle { get { return new DurationModifier(3, 2); } }
		public static DurationModifier DottedDouble { get { return new DurationModifier(7, 4); } }
		public static DurationModifier DottedTriple { get { return new DurationModifier(15, 8); } }

		private readonly int numerator;
		private readonly int denominator;
		public DurationModifier(int numerator, int denominator)
		{
			this.numerator = numerator;
			this.denominator = denominator;
		}

		public StreamTime Apply(StreamTime on)
		{
			return new StreamTime(on.Ticks / denominator * numerator);
		}

		public DurationModifier Clone()
		{
			return (DurationModifier)this.MemberwiseClone();
		}

	}
}

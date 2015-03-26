using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MusicModel
{
	public struct Pitch
	{
		public static readonly int c = 60;
		public static readonly int d = c + 2;
		public static readonly int e = c + 4;
		public static readonly int f = c + 5;
		public static readonly int g = c + 7;
		public static readonly int a = c + 9;
		public static readonly int b = c + 11;

		public int Value { get { return this._value;} }
		private readonly int _value;

		public Pitch(int value)
		{
			if (value < 0) throw new ArgumentOutOfRangeException("value");
			this._value = value;
		}

		public override string ToString()
		{
#warning this method is a bug.
			return (Value % 12).ToString();
		}

#warning that doesn't seem cool
		public static int AddAccidentalToPitchValue(int value, Accidental accidental)
		{
			switch (accidental)
			{
				case Accidental.flat:
					return value - 1;
				case Accidental.sharp:
					return value + 1;
				case Accidental.natural:
					return value;
				default:
					return value;
			}
		}

		public Pitch Clone()
		{
			return (Pitch) this.MemberwiseClone();
		}
	}
}

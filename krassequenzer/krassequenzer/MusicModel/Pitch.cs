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

		public int Value { get { return this._value;} }
		private readonly int _value;

		public Pitch(int value)
		{
			if (value < 0) throw new ArgumentOutOfRangeException("value");
			this._value = value;
		}

		public override string ToString()
		{
			return (Value % 12).ToString();
		}
	}
}

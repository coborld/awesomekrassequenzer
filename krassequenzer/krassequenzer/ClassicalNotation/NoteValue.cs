using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using krassequenzer.Stuff;

namespace krassequenzer.ClassicalNotation
{
	public class NoteValue
	{
		// the supported note values
		public static NoteValue Whole			= new NoteValue(1);
		public static NoteValue Half			= new NoteValue(2);
		public static NoteValue Quarter			= new NoteValue(4);
		public static NoteValue Eigth			= new NoteValue(8);
		public static NoteValue Sixteenth		= new NoteValue(16);
		public static NoteValue ThirtySecond	= new NoteValue(32);
		public static NoteValue SixtyFourth		= new NoteValue(64);

		public static readonly IEnumerable<NoteValue> Supported
		{
			get{
				yield return Whole;
				yield return Half;
				yield return Quarter;
				yield return Eigth;
				yield return Sixteenth;
				yield return ThirtySecond;
				yield return SixtyFourth;
			}
		}

		private int _noteValue = 0;
		private NoteValue(int noteValue)
		{
			// as long as the constructor stays private there is no need to check the validity of the input
			this._noteValue = noteValue;
		}

		public bool isValid()
		{
			return this._noteValue.IsPowerOf2();
		}

		public static bool operator ==(NoteValue l, NoteValue r){
			return l == r;
		}

		public static bool operator >(NoteValue l, NoteValue r)
		{
			return l > r;
		}

		public static bool operator <(NoteValue l, NoteValue r)
		{
			return l < r;
		}

		public static NoteValue operator ++(NoteValue v)
		{
			return new NoteValue(v._noteValue <<= 1);
		}

		public static NoteValue operator --(NoteValue v)
		{
			return new NoteValue(v._noteValue >>= 1);
		}
	}
}

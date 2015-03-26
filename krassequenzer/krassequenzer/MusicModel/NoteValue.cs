using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using krassequenzer.Stuff;

namespace krassequenzer.MusicModel
{
	public class NoteValue
	{
		// the supported note values
		public static NoteValue Whole { get { return new NoteValue(1); } }
		public static NoteValue Half { get { return new NoteValue(2); } }
		public static NoteValue Quarter { get { return new NoteValue(4); } }
		public static NoteValue Eighth { get { return new NoteValue(8); } }
		public static NoteValue Sixteenth { get { return new NoteValue(16); } }
		public static NoteValue ThirtySecond { get { return new NoteValue(32); } }
		public static NoteValue SixtyFourth { get { return new NoteValue(64); } }

		public static List<NoteValue> Supported
		{
			get{
				List<NoteValue> supported = new List<NoteValue>();
				supported.Add(Whole);
				supported.Add(Half);
				supported.Add(Quarter);
				supported.Add(Eighth);
				supported.Add(Sixteenth);
				supported.Add(ThirtySecond);
				supported.Add(SixtyFourth);
				return supported;
			}
		}

		private int _noteValue = 0;
		public int Denominator
		{
			get
			{
				return this._noteValue;
			}
		}

		private DurationModifier _durationModifier;
		public DurationModifier DurationModifier { get { return _durationModifier; } set { durationRecalcRequired = true; _durationModifier = value; } }

		private MusicalTime _duration;
		private bool durationRecalcRequired = true;
		public MusicalTime Duration
		{
			get
			{
				if (durationRecalcRequired)
				{
					_duration = this.getAsMusicalTime();
					durationRecalcRequired = false;
				}
				return _duration;
			}
		}

		private NoteValue(int noteValue)
		{
			// as long as the constructor stays private there is no need to check the validity of the input
			this._noteValue = noteValue;
		}

		public NoteValue(TimeSignature timeSignature) : this(timeSignature.BeatUnit)
		{

		}

		public bool IsValid()
		{
			return this._noteValue.IsPowerOf2();
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

		public override bool Equals(object obj)
		{
			var o = obj as NoteValue;
			if (o == null)
			{
				return false;
			}
			return o._noteValue == this._noteValue;
		}

		public override int GetHashCode()
		{
			return this._noteValue;
		}

		private MusicalTime getAsMusicalTime()
		{
#warning sotix: Why did I built that check?
			if (!IsValid())
			{
				throw new InvalidNoteValueException();
			}

			MusicalTime duration = new MusicalTime(4 * MusicalTime.TicksPerQuarter / Denominator);

			if (DurationModifier != null)
			{
				duration = DurationModifier.Apply(duration);
			}


			return duration;
		}

		public NoteValue Clone()
		{
			return (NoteValue) this.MemberwiseClone();
		}
	}
}

using krassequenzer.MusicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.ClassicalNotation
{
	class ClassicalNote
	{
		public ClassicalPitch Pitch { get; set; }
		public NoteValue NoteValue { get; set; }

		public Modifier Modifier { get; set; }

		/// <summary>
		/// Links to the preceding note, that it is tied to (optional).
		/// </summary>
		public ClassicalNote TiedTo { get; set; }

		public int Voice { get; set; }

		public bool isTied()
		{
			return TiedTo != null;
		}

		public MusicalTime getDuration()
		{
			if (!NoteValue.IsValid())
			{
				throw new InvalidNoteValueException();
			}

			MusicalTime baseValue = new MusicalTime(4 / NoteValue.Denominator * MusicalTime.TicksPerQuarter);

			MusicalTime modified = Modifier.Apply(baseValue);

			return modified;
		}

		public Pitch getPitch()
		{
			return (Pitch)this.Pitch;
		}
	}
}

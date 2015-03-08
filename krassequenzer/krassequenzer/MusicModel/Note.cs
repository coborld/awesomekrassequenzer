using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MusicModel
{
	public class Note
	{

		public Note()
		{

		}

		public Note(int relativeNoteLength)
		{
			this.RelativeNoteLength = relativeNoteLength;
		}

		public MusicalTime Duration { get; set; }

		public MusicalTime StartPosition { get; set; }

		public int Voice { get; set; }

		public Pitch Pitch { get; set; }

		public static List<Note> StandardNotes = new List<Note>(
			new Note[]{
				new Note(1),
				new Note(2),
				new Note(4),
				new Note(8),
				new Note(16),
				new Note(32)
			});




		private int _relativeNoteLength = 0;
		public int RelativeNoteLength
		{
			get
			{
				return this._relativeNoteLength;
			}
			set
			{
				this.Duration = MusicalTime.getByBeatUnit(value);
				this._relativeNoteLength = value;
			}
		}

		public Note Clone()
		{
			return (Note)this.MemberwiseClone();
		}


		public String toString()
		{
			string sep = ";";
			return "(" + Pitch + sep + RelativeNoteLength + ")";
		}

		public static int Comparison(Note l, Note r)
		{
			return MusicalTime.Comparison(l.StartPosition, r.StartPosition);
		}
	}
}

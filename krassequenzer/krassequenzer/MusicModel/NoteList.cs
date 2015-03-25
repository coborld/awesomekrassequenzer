using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MusicModel
{
	/// <summary>
	/// You can put Notes in here and let the durations and startpositions get calculated.
	/// </summary>
	class NoteList : List<Note>
	{
		private List<Note> innerList = new List<Note>();

		MusicalTime nextStart = MusicalTime.Zero;

		public new void Add(Note note)
		{
			if (innerList.Count > 0)
			{

			}

			List<TiedNote> tiedNotesClone = new List<TiedNote>();
			foreach (var tn in note.TiedNotes)
			{
				tiedNotesClone.Add(tn.Clone());
			}
			Note toAdd = new Note()
			{
				Pitch = note.Pitch.Clone(),
				NoteValue = note.NoteValue.Clone(),
				ScoreStartPosition = nextStart,
				TiedNotes = tiedNotesClone
			};

			innerList.Add(toAdd);

			nextStart = toAdd.ScoreStartPosition + toAdd.ScoreDuration;
		}

		public void AddPause(MusicalTime length)
		{
			nextStart += length;
		}

		

	}
}

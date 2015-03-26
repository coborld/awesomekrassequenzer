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
	public class NoteList : List<Note>
	{
		private List<Note> innerList = new List<Note>();

		MusicalTime nextStart = MusicalTime.Zero;

		private int mainVoice;
		public NoteList(int mainVoice)
		{
			this.mainVoice = mainVoice;
		}

		public new void Add(Note note)
		{
			
			List<TiedNote> tiedNotesClone = new List<TiedNote>();
			foreach (var tn in note.TiedNotes)
			{
				tiedNotesClone.Add(tn.Clone());
			}

			Note toAdd = note.Clone();
			toAdd.ScoreStartPosition = nextStart;
			//Note toAdd = new Note()
			//{
			//	NoteValue = note.NoteValue.Clone(),
			//	Pitch = note.Pitch.Clone(),
			//	ScoreStartPosition = nextStart,
			//	TiedNotes = tiedNotesClone,
			//	Voice = note.Voice
			//};

			innerList.Add(toAdd);

			if(note.Voice == mainVoice ){
				nextStart = toAdd.ScoreStartPosition + toAdd.ScoreDuration;
			}
		}

		public void AddPause(MusicalTime length)
		{
			nextStart += length;
		}

		

	}
}

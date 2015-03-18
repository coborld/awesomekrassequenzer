using krassequenzer.ClassicalNotation;
using krassequenzer.MusicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.GenerationStuff
{
	public class AwesomeGenerator
	{
		public AwesomeGenerator()
		{

		}

		public int NrNotesToGenerate { get; set; }


		public void Generate(Track part)
		{
			Random rnd = new Random(0);
			MusicalTime curStart = MusicalTime.Zero;
			if (NrNotesToGenerate <= 0)
			{
				NrNotesToGenerate = 10;
			}
			// !! cool:
			// part.Notes.AddRange(Enumerable.Repeat(0, this.NrNotesToGenerate).Select(x => new Note() { Duration = rnd.Next(1, this.MaxDuration)}));
			for (int i = 0; i < NrNotesToGenerate; i++)
			{
				//
				// create a classical note
				//
				int rndNoteValueIndex = rnd.Next( NoteValue.Supported.Count() );
				NoteValue nv = NoteValue.Supported.ToList()[rndNoteValueIndex].Clone();
				Pitch pitch = new Pitch( Pitch.c );

				//
				// create the internal note
				//
				MusicalTime duration = nv.getAsMusicalTime();
				MusicalTime newStart = curStart + duration;
				part.Notes.Add(new Note { ScoreStartPosition = curStart, NoteValue = nv, Pitch = pitch });
				curStart = newStart;
			}
		}
	}
}

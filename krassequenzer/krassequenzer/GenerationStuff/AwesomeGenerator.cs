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
				ClassicalNote cn = new ClassicalNote();
				int rndNoteValueIndex = rnd.Next( NoteValue.Supported.Count() );
				cn.NoteValue = NoteValue.Supported.ToList()[rndNoteValueIndex];
				cn.Pitch = new Pitch( Pitch.c );

				//
				// create the internal note
				//
				MusicalTime duration = cn.getDuration();
				MusicalTime newStart = curStart + duration;
				part.Notes.Add(new Note { ScoreStartPosition = curStart, ScoreDuration = duration, Pitch = cn.getPitch() });
				curStart = newStart;
			}
		}
	}
}

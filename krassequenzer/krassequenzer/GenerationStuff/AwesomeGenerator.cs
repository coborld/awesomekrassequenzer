﻿using krassequenzer.MusicModel;
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

		//public Beat Beat;

		public void Generate(Track part)
		{
			Random rnd = new Random(0);
			// !! cool:
			// part.Notes.AddRange(Enumerable.Repeat(0, this.NrNotesToGenerate).Select(x => new Note() { Duration = rnd.Next(1, this.MaxDuration)}));
			for (int i = 0; i < NrNotesToGenerate; i++)
			{
				int rndNoteIndex = rnd.Next( Note.StandardNotes.Count );
				part.Notes.Add( Note.StandardNotes[rndNoteIndex].Clone() );
			}
		}
	}
}

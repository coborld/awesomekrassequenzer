﻿using System;
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

		public bool DisplayInScore = true;

		public MusicalTime ScoreDuration { get; set; }
		public MusicalTime DurationOffset { get; set; }

		public MusicalTime ScoreStartPosition { get; set; }
		public MusicalTime StartPositionOffset { get; set; }
		
		public int Voice { get; set; }

		public Pitch Pitch { get; set; }

		public Note Clone()
		{
			return (Note)this.MemberwiseClone();
		}


		public override string ToString()
		{
			string sep = "; ";
			return "(" + this.Pitch + sep + this.ScoreDuration + ")";
		}

		public static int Comparison(Note l, Note r)
		{
			return MusicalTime.Comparison(l.ScoreStartPosition, r.ScoreStartPosition);
		}
	}
}

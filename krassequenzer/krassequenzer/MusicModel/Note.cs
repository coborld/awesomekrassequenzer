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

		public MusicalTime Duration { get; set; }

		public MusicalTime StartPosition { get; set; }

		public int Voice { get; set; }

		public Pitch Pitch { get; set; }

		public Note Clone()
		{
			return (Note)this.MemberwiseClone();
		}


		public override string ToString()
		{
			string sep = "; ";
			return "(" + this.Pitch + sep + this.Duration + ")";
		}

		public static int Comparison(Note l, Note r)
		{
			return MusicalTime.Comparison(l.StartPosition, r.StartPosition);
		}
	}
}

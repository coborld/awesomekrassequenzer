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

		public Dotting Dotting { get; set; }

		/// <summary>
		/// Links to the preceding note, that it is tied to (if it is).
		/// </summary>
		public ClassicalNote TiedTo { get; set; }


		public bool isTied()
		{
			return TiedTo != null;
		}

		
	}
}

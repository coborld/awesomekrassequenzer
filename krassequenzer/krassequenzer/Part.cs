using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer
{
	public class Part
	{
		private readonly List<Note> notes = new List<Note>();
		public List<Note> Notes { get { return this.notes; } }

		public override string ToString()
		{
			return String.Join(", ", this.notes.Select(x => x.Duration));
		}
	}
}
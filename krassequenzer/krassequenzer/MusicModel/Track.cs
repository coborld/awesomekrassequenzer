using krassequenzer.Stuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MusicModel
{
	public class Track
	{
		public string Name { get; set; }

		private readonly OrderedCollection<Note> _notes = new OrderedCollection<Note>((x, y) => Note.Comparison(x, y));
		public List<Note> Notes { get { return this._notes.ToList<Note>(); } }

		public String toString()
		{
			MusicalTime prevEnd = MusicalTime.Zero;
			StringBuilder sb = new StringBuilder();
			sb.Append("\"");
			sb.Append(Name);
			sb.Append("\"");
			sb.Append(Environment.NewLine);
			sb.Append("   ");
			string sep = " ";
			foreach (Note note in Notes)
			{
				sb.Append(sep);
				if (note.StartPosition > prevEnd)
				{
					sb.Append("[");
					sb.Append(((note.StartPosition - prevEnd).Ticks).ToString());
					sb.Append(" ticks]");
					sb.Append(sep);
				}
				sb.Append(note);
			}
			return sb.ToString();
		}
	}
}
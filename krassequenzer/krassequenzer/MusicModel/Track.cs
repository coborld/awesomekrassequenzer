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
		public ICollection<Note> Notes { get { return this._notes; } }

		private readonly OrderedCollection<ProgramChange> _programChanges = new OrderedCollection<ProgramChange>((x, y) => ProgramChange.TimeComparison(x, y));
		public ICollection<ProgramChange> ProgramChanges { get { return this._programChanges; } }

		/// <summary>
		/// Gets or sets the MIDI channel that is used for all events in this track which do not have their MIDI channel specified.
		/// </summary>
		public MidiChannelIndex DefaultMidiChannel { get; set; }

		public override string ToString()
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
				if (note.ScoreStartPosition > prevEnd)
				{
					sb.Append("[");
					sb.Append(((note.ScoreStartPosition - prevEnd).Ticks).ToString());
					sb.Append(" ticks]");
					sb.Append(sep);
				}
				prevEnd = note.ScoreStartPosition + note.ScoreDuration;
				sb.Append(note);
			}
			return sb.ToString();
		}
	}
}
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
			this.Voice = 0;
			this.DisplayInScore = true;
		}

		public bool DisplayInScore { get; set; }

		public MusicalTime ScoreDuration { get { return CalcScoreDuration(); } }
		public MusicalTime DurationOffset { get; set; }

		public MusicalTime ScoreStartPosition { get; set; }
		public MusicalTime StartPositionOffset { get; set; }
		
		public int Voice { get; set; }

		public MidiChannelIndex MidiChannelIndex { get; set; }
		public MidiVelocity NoteOnVelocity { get; set; }
		public MidiVelocity NoteOffVelocity { get; set; }

		public Pitch Pitch { get; set; }

		public NoteValue NoteValue { get; set; }

		public List<TiedNote> TiedNotes { get; set; }

		public Note Clone()
		{
			var theClone = (Note)this.MemberwiseClone();
			theClone.TiedNotes = new List<TiedNote>();
			this.TiedNotes.ForEach(x => theClone.TiedNotes.Add(x.Clone()));
			return theClone;
		}


		public override string ToString()
		{
			string sep = "; ";
			return "(" + this.ScoreStartPosition + sep + this.Pitch + sep + this.ScoreDuration + ")";
		}

		public static int Comparison(Note l, Note r)
		{
			return MusicalTime.Comparison(l.ScoreStartPosition, r.ScoreStartPosition);
		}

		private MusicalTime CalcScoreDuration()
		{
			MusicalTime baseDuration = NoteValue.Duration;
			return baseDuration;
			MusicalTime sumOfTiedNotes = new MusicalTime(TiedNotes.Sum(x => x.NoteValue.Duration.Ticks));

			return baseDuration + sumOfTiedNotes;
		}

	}

	/// <summary>
	/// Represents a MIDI channel index in the range 0 .. 15.
	/// </summary>
	public struct MidiChannelIndex
	{
		/// <summary>
		/// Initializes a new instance.
		/// </summary>
		/// <param name="index">The channel index. Must be in the range 0 .. 15.</param>
		public MidiChannelIndex(int index)
		{
			if (index < 0 || index > 15) throw new ArgumentOutOfRangeException("index");
			this._index = index;
		}

		private readonly int _index;
		/// <summary>
		/// Gets the channel index.
		/// </summary>
		public int Index { get { return this._index; } }
	}

	/// <summary>
	/// Represents a MIDI note velocity in the range 0 .. 127.
	/// </summary>
	public struct MidiVelocity
	{
		/// <summary>
		/// Initializes a new instance.
		/// </summary>
		/// <param name="velocity">The velocity. Must be in the range 0 .. 127.</param>
		public MidiVelocity(int velocity)
		{
			if (velocity < 0 || velocity > 127) throw new ArgumentOutOfRangeException("velocity");
			this._velocity = velocity;
		}

		private readonly int _velocity;
		/// <summary>
		/// Gets the velocity value.
		/// </summary>
		public int Velocity { get { return this._velocity; } }
	}
}

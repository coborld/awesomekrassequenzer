using krassequenzer.MidiPlayback;
using krassequenzer.MusicModel;
using krassequenzer.PlaybackStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrassequenzerTester
{
	class PinoPlaybackTest
	{
		public Composition createMidiDemo()
		{
			// build the composition
			var theDemo = new Composition();
			theDemo.Title = "MidiDemo";

			theDemo.TempoTrack.InitialTempo = 100;

			// add the track
#warning NoteList is the bug
			List<Note> theNotes = new NoteList(0);
			

			// put something in the track

			// set the scale
			var scale = new Dictionary<char, Pitch>();
			scale.Add('h', new Pitch(Pitch.AddAccidentalToPitchValue(Pitch.b, Accidental.flat)));
			scale.Add('a', new Pitch(Pitch.AddAccidentalToPitchValue(Pitch.a, Accidental.flat)));
			scale.Add('e', new Pitch(Pitch.AddAccidentalToPitchValue(Pitch.e, Accidental.flat)));
			scale.Add('c', new Pitch(Pitch.c));
			scale.Add('d', new Pitch(Pitch.d));
			scale.Add('f', new Pitch(Pitch.f));
			scale.Add('g', new Pitch(Pitch.g));

			long currentStartPosition = 0;

			Func<NoteValue, Pitch, Note> noter = (noteValue, pitch) =>
				{
					var note = new Note();
					note.NoteValue = noteValue;
					note.Pitch = pitch;
					note.ScoreStartPosition = new MusicalTime(currentStartPosition);
					note.NoteOnVelocity = new MidiVelocity(100);
					currentStartPosition += note.ScoreDuration.Ticks;
					return note;
				};

			
			// Takt 1
			theNotes.Add(noter(NoteValue.Eighth, scale['e']));
			theNotes.Add(noter(NoteValue.Eighth, scale['f']));

			// Takt 2
			theNotes.Add(noter(NoteValue.Quarter, scale['g']));
			theNotes.Add(noter(NoteValue.Eighth, scale['a']));
			theNotes.Add(noter(NoteValue.Eighth, scale['g']));
			NoteValue dottedQuarter = NoteValue.Quarter;
			dottedQuarter.DurationModifier = DurationModifier.DottedSingle;
			theNotes.Add(noter(dottedQuarter, scale['f']));
			theNotes.Add(noter(NoteValue.Eighth, scale['e']));

			// Takt 3
			theNotes.Add(noter(NoteValue.Eighth, scale['e']));
			theNotes.Add(noter(NoteValue.Eighth, scale['d']));
			theNotes.Add(noter(NoteValue.Quarter, scale['c']));
			currentStartPosition += NoteValue.Quarter.Duration.Ticks;
			theNotes.Add(noter(NoteValue.Eighth, scale['c']));
			theNotes.Add(noter(NoteValue.Eighth, scale['d']));

			// Takt 4
			theNotes.Add(noter(NoteValue.Quarter, scale['e']));
			theNotes.Add(noter(NoteValue.Eighth, scale['d']));
			theNotes.Add(noter(NoteValue.Eighth, scale['c']));
			theNotes.Add(noter(NoteValue.Quarter, scale['d']));
			theNotes.Add(noter(NoteValue.Quarter, scale['e']));
			//theNotes.Add(noter(NoteValue.Quarter, new Pitch(Pitch.h - 12), Voice = 1));
			//theNotes.Add(noter(NoteValue.Quarter, scale['f'], Voice = 2));

			theNotes.Add(noter(NoteValue.Whole, scale['c']));

			// stuff the notes in the Track
			var aTrack = new Track();
			theNotes.ForEach(x => aTrack.Notes.Add(x));
			theDemo.Tracks.Add(aTrack);

			aTrack.ProgramChanges.Add(new ProgramChange() { Instrument = (int)MidiGMInstrumentSet.Lead_1_square });

			var player = new CompositionPlayer();
			player.Play(theDemo).Wait();

			return theDemo;
		}
	}
}

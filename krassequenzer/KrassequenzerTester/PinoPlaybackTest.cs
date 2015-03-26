using krassequenzer.MusicModel;
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

			// add the track
			var theNotes = new NoteList(0);
			

			// put something in the track

			// set the scale
			var scale = new Dictionary<char, Pitch>();
			scale.Add('h', new Pitch(Pitch.AddAccidentalToPitchValue(Pitch.h, Accidental.flat)));
			scale.Add('a', new Pitch(Pitch.AddAccidentalToPitchValue(Pitch.a, Accidental.flat)));
			scale.Add('e', new Pitch(Pitch.AddAccidentalToPitchValue(Pitch.e, Accidental.flat)));
			scale.Add('c', new Pitch(Pitch.c));
			scale.Add('d', new Pitch(Pitch.d));
			scale.Add('f', new Pitch(Pitch.f));
			scale.Add('g', new Pitch(Pitch.g));

			
			// Takt 1
			theNotes.Add(new Note() { NoteValue = NoteValue.Eigth, Pitch = scale['e'] });
			theNotes.Add(new Note() { NoteValue = NoteValue.Eigth, Pitch = scale['f'] });

			// Takt 2
			theNotes.Add(new Note() { NoteValue = NoteValue.Quarter, Pitch = scale['g'] });
			theNotes.Add(new Note() { NoteValue = NoteValue.Eigth, Pitch = scale['a'] });
			theNotes.Add(new Note() { NoteValue = NoteValue.Eigth, Pitch = scale['g'] });
			NoteValue dottedEigth = NoteValue.Eigth;
			dottedEigth.DurationModifier = DurationModifier.DottedSingle;
			theNotes.Add(new Note() { NoteValue = dottedEigth, Pitch = scale['f'] });
			theNotes.Add(new Note() { NoteValue = NoteValue.Eigth, Pitch = scale['e'] });

			// Takt 3
			theNotes.Add(new Note() { NoteValue = NoteValue.Eigth, Pitch = scale['e'] });
			theNotes.Add(new Note() { NoteValue = NoteValue.Eigth, Pitch = scale['d'] });
			theNotes.Add(new Note() { NoteValue = NoteValue.Quarter, Pitch = scale['c'] });
			theNotes.AddPause(NoteValue.Quarter.Duration);
			theNotes.Add(new Note() { NoteValue = NoteValue.Eigth, Pitch = scale['c'] });
			theNotes.Add(new Note() { NoteValue = NoteValue.Eigth, Pitch = scale['d'] });

			// Takt 4
			theNotes.Add(new Note() { NoteValue = NoteValue.Quarter, Pitch = scale['e'] });
			theNotes.Add(new Note() { NoteValue = NoteValue.Eigth, Pitch = scale['d'] });
			theNotes.Add(new Note() { NoteValue = NoteValue.Eigth, Pitch = scale['c'] });
			theNotes.Add(new Note() { NoteValue = NoteValue.Quarter, Pitch = scale['d'] });
			theNotes.Add(new Note() { NoteValue = NoteValue.Quarter, Pitch = scale['e'] });
			theNotes.Add(new Note() { NoteValue = NoteValue.Quarter, Pitch = new Pitch(Pitch.h - 12), Voice = 1 });
			theNotes.Add(new Note() { NoteValue = NoteValue.Quarter, Pitch = scale['f'], Voice = 2 });


			// stuff the notes in the Track
			var aTrack = new Track();
			theNotes.ForEach(x => aTrack.Notes.Add(x));
			theDemo.Tracks.Add(aTrack);

			return theDemo;
		}
	}
}

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
			var theTrack = new Track();
			theDemo.Tracks.Add(theTrack);

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
			theTrack.Notes.Add(new Note() { NoteValue = NoteValue.Eigth, Pitch = scale['e'] });
			theTrack.Notes.Add(new Note() { NoteValue = NoteValue.Eigth, Pitch = scale['f'] });

			// Takt 2
			theTrack.Notes.Add(new Note() { NoteValue = NoteValue.Quarter, Pitch = scale['g'] });
			theTrack.Notes.Add(new Note() { NoteValue = NoteValue.Eigth, Pitch = scale['a'] });
			theTrack.Notes.Add(new Note() { NoteValue = NoteValue.Eigth, Pitch = scale['g'] });
			NoteValue dottedEigth = NoteValue.Eigth.Clone();
			dottedEigth.DurationModifier = DurationModifier.DottedSingle;
			theTrack.Notes.Add(new Note() { NoteValue = dottedEigth, Pitch = scale['g'] });
			theTrack.Notes.Add(new Note() { NoteValue = NoteValue.Eigth, Pitch = scale['f'] });

			return theDemo;
		}
	}
}

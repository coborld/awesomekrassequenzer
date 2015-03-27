using krassequenzer.MidiPlayback;
using krassequenzer.MusicModel;
using krassequenzer.PlaybackStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using krassequenzer.Stuff;
using krassequenzer.DeviceSettings;

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
			// stuff the notes in the Track
			var aTrack = new Track();
			var theNotes = aTrack.Notes;
			theDemo.Tracks.Add(aTrack);
			
			// put something in the track
			aTrack.ProgramChanges.Add(new ProgramChange() { Instrument = (int)MidiGMInstrumentSet.Koto });

			// set the scale
			var scale = new Dictionary<char, Pitch>();
			scale.Add('b', new Pitch(Pitch.AddAccidentalToPitchValue(Pitch.b, Accidental.flat)));
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
					note.Pitch = pitch + 0;
					note.ScoreStartPosition = new StreamTime(currentStartPosition);
					note.NoteOnVelocity = new MidiVelocity(90);
					currentStartPosition += note.ScoreDuration.Ticks;
					return note;
				};

			// startup offset
			currentStartPosition += NoteValue.Half.Duration.Ticks;
			
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
			//theNotes.Add(noter(NoteValue.Quarter, scale['e']));
			theNotes.Add(noter(NoteValue.Quarter, new Pitch(Pitch.b - 12)));
			//theNotes.Add(noter(NoteValue.Quarter, scale['f'], Voice = 2));

			theNotes.Add(noter(NoteValue.Whole, scale['c']));

			
			//
			// the other track
			//
			var bTrack = new Track();
			bTrack.Name = "bTrack";
			bTrack.ProgramChanges.Add(new ProgramChange() { Instrument = (int)MidiGMInstrumentSet.Cello });
			bTrack.DefaultMidiChannel = new MidiChannelIndex(1);
			theDemo.Tracks.Add(bTrack);
			theNotes = bTrack.Notes;

			currentStartPosition = 0;
			Action<NoteValue> advance = nv => currentStartPosition += nv.Duration.Ticks;

			noter = (noteValue, pitch) =>
				{
					var note = new Note();
					note.NoteValue = noteValue;
					note.Pitch = pitch;
					note.ScoreStartPosition = new StreamTime(currentStartPosition);
					note.NoteOnVelocity = new MidiVelocity(75);
					return note;
				};

			advance(NoteValue.Half);
			advance(NoteValue.Quarter);

			theNotes.Add(noter(NoteValue.Half, scale['a'] - 24));
			theNotes.Add(noter(NoteValue.Half, scale['e'] - 12));
			advance(NoteValue.Half);

			theNotes.Add(noter(NoteValue.Half, scale['b'] - 24));
			theNotes.Add(noter(NoteValue.Half, scale['f'] - 12));
			advance(NoteValue.Half);

			theNotes.Add(noter(NoteValue.Whole, scale['c'] - 12));
			theNotes.Add(noter(NoteValue.Whole, scale['g'] - 12));
			advance(NoteValue.Whole);

			theNotes.Add(noter(NoteValue.Half, scale['a'] - 24));
			theNotes.Add(noter(NoteValue.Half, scale['e'] - 12));
			advance(NoteValue.Half);

			theNotes.Add(noter(NoteValue.Half, scale['g'] - 24));
			theNotes.Add(noter(NoteValue.Half, scale['d'] - 12));
			advance(NoteValue.Half);

			theNotes.Add(noter(NoteValue.Whole, scale['c'] - 12));
			theNotes.Add(noter(NoteValue.Whole, scale['g'] - 12));
			advance(NoteValue.Whole);


			// output device setup
			var midiOutDeviceId = (int)MidiSystemInfo.Query().OutDeviceInfo.First(x => x.Name.Contains("Microsoft")).Id;
			var deviceSetup = new DeviceSetup(midiOutDeviceId);

			var player = new CompositionPlayer();
			player.Play(theDemo, deviceSetup).Wait();

			return theDemo;
		}
	}
}

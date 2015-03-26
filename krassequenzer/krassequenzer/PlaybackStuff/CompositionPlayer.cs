using krassequenzer.MidiPlayback;
using krassequenzer.MusicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using krassequenzer.Stuff;
using System.Threading;

namespace krassequenzer.PlaybackStuff
{
	public class CompositionPlayer
	{
		public async Task Play(Composition composition)
		{
			composition.NotNull("composition");
			// TODO: pass in the output interface
			// TODO: pass in the CancellationToken
			var ct = CancellationToken.None;

			// TODO: tempo

			var systemInfo = MidiSystemInfo.Query();
			var msSynth = systemInfo.OutDeviceInfo.Single(x => x.Name.Contains("Microsoft"));

			// convert each track to a series of midi events
			var midiEvents = composition.Tracks.Select(x => ConvertTrack(x)).ToArray();
			
			// merge the events from all tracks into a single stream
			var mergedEvents = MidiStreamEventMerger.Merge(midiEvents);

			// now everything is in one list - play the shit!
			using (var stream = new MidiOutStream(msSynth))
			{
				await stream.Play(mergedEvents, ct);
			}
		}

		private static IEnumerable<IMidiStreamEvent> ConvertTrack(Track t)
		{
			var f = new MidiStreamEventFactory();
			long currentTime = 0;
			// each time we encounter a note, we add it to this list so that we can produce the note off events for it later
			var pendingNoteOffEvents = new List<Note>();
			Action<IEnumerable<Note>> insertNoteOffEvents = notes =>
				{
					foreach (var noteOff in notes)
					{
						f.NoteOff((uint)(noteOff.ScoreStartPosition.Ticks + noteOff.ScoreDuration.Ticks - currentTime), noteOff.MidiChannelIndex.Index, noteOff.Pitch.Value, noteOff.NoteOffVelocity.Velocity);
						currentTime = noteOff.ScoreStartPosition.Ticks;
					}
				};

			foreach (var note in t.Notes)
			{
				// insert pending note off events that happened before this event first
				insertNoteOffEvents(pendingNoteOffEvents.Where(x => x.ScoreStartPosition + x.ScoreDuration <= note.ScoreStartPosition));
				f.NoteOn((uint)(note.ScoreStartPosition.Ticks - currentTime), note.MidiChannelIndex.Index, note.Pitch.Value, note.NoteOnVelocity.Velocity);
				pendingNoteOffEvents.Add(note);
				currentTime = note.ScoreStartPosition.Ticks;
			}

			// put in all the remaining note off events
			insertNoteOffEvents(pendingNoteOffEvents);

			return f.Events;
		}
	}
}

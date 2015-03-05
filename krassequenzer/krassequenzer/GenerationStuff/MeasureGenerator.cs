using krassequenzer.MusicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.GenerationStuff
{
	class MeasureGenerator : Generator
	{
		private TimeSignature timeSignature;
		private ProbabilityConfiguration probConfig;

		public MeasureGenerator(TimeSignature timeSignature, ProbabilityConfiguration probConfig)
		{
			if (timeSignature.BeatUnit > 32)
			{
				throw new NotSupportedException();
			}
			this.timeSignature = timeSignature;
			this.probConfig = probConfig;
		}

		public List<Note> Generate()
		{

			List<Note> generatedNotes = new List<Note>();
			Random rnd = new Random();

			MusicalTime measureLength = new MusicalTime(timeSignature.Beats * MusicalTime.getByBeatUnit(timeSignature.BeatUnit).Ticks);

			// find the longest note, that fits in the measure
#warning CheckUseful: once the generator is working, check if biggestFitIndex is used
			int biggestFitIndex = -1;
			List<Note> fittingNotes = new List<Note>();
			for (int i = 0; i < Note.StandardNotes.Count; i++)
			{
				Note note = Note.StandardNotes[i];
				if (note.Duration <= measureLength)
				{
					fittingNotes.Add(note.Clone());
					biggestFitIndex = i;
					break;
				}
			}
			
			if (biggestFitIndex < 0)
			{
				/* 
				 * At this point the TimeSignature was so small, that there wasn't a note that
				 * would fit in there.
				 * This shouldn't happen because we check the TimeSignature in the constructor.
				 */
				// throw new WeFuckedUpException();
			}

			bool measureIsFull = false;

#warning TODO: implement generation of notes that span over multiple measures
			// Indication if the next note would fall on the beat set by the TimeSignature.
			// Currently a measure starts with a note on the beat, since there is no knowledge about previous measures.
			bool atRythm = true;

			while (!measureIsFull)
			{
				Note nextNote;
				// check if we are currently in offbeat
				if (atRythm)
				{
					// we are not in offbeat, so let's check if we should start one
					if (!(rnd.Next(100) < probConfig.Offbeat))
					{
						// no new offbeat, so add to the beat
						nextNote = new Note(timeSignature.BeatUnit);

					}
					else
					{
						// start an offbeat

						// let's decide what offbeat-method we choose
						if (rnd.Next(100) < probConfig.Dotting)
						{
							throw new NotImplementedException();
						}
						else
						{
							bool nextIsOnBeat = false;
							bool nextFits = false;
						}
					}
				}
				else
				{
#warning TODO: how get out of the syncopation
				}

#warning FIXME: calculate measureIsFull and atRythm
				// measureIsFull = ...
				// atRythm = ...
			}

			return generatedNotes;
		}


	}
}

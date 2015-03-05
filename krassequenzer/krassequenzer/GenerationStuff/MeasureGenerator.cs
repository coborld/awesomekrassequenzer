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
			Note biggestFit = null;
			foreach( Note note in Note.StandardNotes){
				if (note.Duration <= measureLength)
				{
					biggestFit = note;
					break;
				}
			}
			if (biggestFit == null)
			{
				/* 
				 * At this point the TimeSignature was so small, that there wasn't a note that
				 * would fit in there.
				 * This shouldn't happen because we check the TimeSignature in the constructor.
				 */ 
				// throw new WeFuckedUpException();
			}

			int probOfBiggest = probConfig.NoteProbs.getProbabilityByRelativeNodeLength(biggestFit.RelativeNoteLength);

			bool measureIsFull = false;
			
#warning TODO: implement generation of notes that span over multiple measures
			// Indication if the next note would fall on the beat set by the TimeSignature.
			// Currently a measure starts with a note on the beat, since there is no knowledge about previous measures.
			bool atRythm = true;

			while (!measureIsFull)
			{
				Note nextNote;
				// check if we are in syncopation
				if (atRythm)
				{
					// we are not in syncopation, so let's check if we should start one
					if (!(rnd.Next(100) < probConfig.Syncopation))
					{
						// no new syncopation, so add to the beat
						nextNote = new Note(timeSignature.BeatUnit);

					}
					else
					{
						// start a syncopation

						// let's decide what syncopation-method we choose
						if (rnd.Next(100) < probConfig.Dotting)
						{
							throw new NotImplementedException();
						}
						else
						{
							bool nextIsOnBeat = false;
							bool nextFits;
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

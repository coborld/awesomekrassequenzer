using krassequenzer.MusicModel;
using krassequenzer.Randomisation;
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

			StreamTime measureLength = new StreamTime(timeSignature.Beats * StreamTime.getByRelativeNoteLength(timeSignature.BeatUnit).Ticks);

			// find the longest note, that fits in the measure
#warning CheckUseful: once the generator is working, check if biggestFitIndex is used
			int biggestFitIndex = -1;
			List<ObjectWithProbability<NoteValue>> fittingNoteValues = new List<ObjectWithProbability<NoteValue>>();
			for (int i = 0; i < NoteValue.Supported.Count; i++)
			{
				NoteValue nv = NoteValue.Supported[i];
				if (nv.Duration <= measureLength)
				{
					int noteProb = probConfig.NoteProbs.getProbabilityByRelativeNodeLength(nv.Denominator);
					fittingNoteValues.Add(new ObjectWithProbability<NoteValue>(noteProb, nv));
					biggestFitIndex = i;
					break;
				}
			}
			
			if (biggestFitIndex < 0)
			{
				// 
				// At this point the TimeSignature was so small, that there wasn't a note that
				// would fit in there.
				// This shouldn't happen because we check the TimeSignature in the constructor.
				//
				// throw new WeFuckedUpException();
			}

			StreamTime remainingTimeInMeasure = measureLength;
			StreamTime nextOnBeat = remainingTimeInMeasure;
			StreamTime smallestPossibleDuration = NoteValue.Supported.Last().Duration;

#warning TODO: implement generation of notes that span over multiple measures
			// Indication if the next note would fall on the beat set by the TimeSignature.
			// Currently a measure starts with a note on the beat, since there is no knowledge about previous measures.
			bool atRythm = true;

			

			List<ObjectWithProbability<NoteValue>> notesSmallerThanBeatsUnit = new List<ObjectWithProbability<NoteValue>>();
			foreach (ObjectWithProbability<NoteValue> nv in fittingNoteValues)
			{
				if (nv.TheObject.Denominator < timeSignature.BeatUnit)
				{
					notesSmallerThanBeatsUnit.Add(nv);
				}
			}
			ObjectWithProbability<Object> offbeatPlayer = new ObjectWithProbability<object>(probConfig.Offbeat, null);
			ObjectWithProbability<Object> dottingPlayer = new ObjectWithProbability<object>(probConfig.Dotting, null);
			ObjectWithProbability<Object> tripletPlayer = new ObjectWithProbability<object>(probConfig.Triplet, null);
			while (remainingTimeInMeasure > StreamTime.Zero)
			{
				NoteValue nextNoteValue; // to be generated

				if (remainingTimeInMeasure < smallestPossibleDuration)
				// we don't have a small enough note to put in the measure
				{
					// problem in the algorithm
					throw new NotImplementedException();
				}
				else if (remainingTimeInMeasure == smallestPossibleDuration)
				// there is just enough space for the smallest note we have
				{
					nextNoteValue = NoteValue.Supported.Last().Clone();
					nextOnBeat = nextOnBeat - nextNoteValue.Duration; // should be 0
				}
				else
				{

					// check if we are currently in offbeat
					if (atRythm)
					{
						// we are not in offbeat, so let's check if we should start one
						if (!(offbeatPlayer.Play(rnd)))
						{
							// no new offbeat, so add to the beat
							nextNoteValue = new NoteValue(timeSignature);

							nextOnBeat = nextOnBeat - nextNoteValue.Duration;

						}
						else
						{
							//
							// start an offbeat
							//

							// let's decide what offbeat-method we choose
							if (dottingPlayer.Play(rnd))
							{
								throw new NotImplementedException();
							}
							else
							{
								// 
								// Insert a note that is shorter than the beats' unit.
								// There could be a longer note as well, but that would require
								// the next measure to know about it, which is not implemented
								// yet.
								// 

								// choose a Note
								nextNoteValue = RandomUtil.PlayMultiple<NoteValue>(notesSmallerThanBeatsUnit, rnd).Clone();

								// indicate that the next note will be offbeat
								atRythm = false;
								nextOnBeat = nextOnBeat - StreamTime.getByRelativeNoteLength(timeSignature.BeatUnit);
								
							}
						}

					}
					else
					// not atRythm
					{
#warning TODO: how get out of the offbeat
						nextNoteValue = null; // just for the compiler; replace with real code!

						
					}

					//
					// now really add the nextNote to the measure
					//
					Note noteToAdd = new Note();
					noteToAdd.NoteValue = nextNoteValue;
					generatedNotes.Add(noteToAdd);

					remainingTimeInMeasure -= nextNoteValue.Duration;

				}
			}

			return generatedNotes;
			
		}



	}
}

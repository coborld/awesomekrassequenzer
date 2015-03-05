using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.GenerationStuff
{
	class ProbabilityConfiguration
	{
		private NoteProbabilites _noteProbs;
		public NoteProbabilites NoteProbs
		{
			get
			{
				return this._noteProbs;
			}
			set
			{
				if (value.checkAddUpTo100())
				{
					this._noteProbs = value;
				}
				else
				{
					throw new NoteProbabilityConfigurationException("Probabilities don't add up to 100.");
				}
			}
		}
		
		// the following should be the in range [0-100]
#warning TODO: validate the probabilitiess
		public int Offbeat { get; set; }
		public int Dotting { get; set; }
		public int Triplet { get; set; }

	}
}

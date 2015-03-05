using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.GenerationStuff
{
	class NoteProbabilites
	{
		private Dictionary<int, int> probs = new Dictionary<int, int>();

		public void setByRelativeNodeLength(int relativeNodeLength, int probability){
#warning Unchecked: there should be a validation of the relativeNodeLength
			probs.Add(relativeNodeLength, probability);
		}

		public int getProbabilityByRelativeNodeLength(int relativeNodeLength)
		{
#warning Unchecked: there should be a validation of the relativeNodeLength
			return probs.ContainsKey(relativeNodeLength) ? probs[relativeNodeLength] : 0;
		}

		public bool checkAddUpTo100()
		{
			return probs.Values.Sum() == 100;
		}
	}
}

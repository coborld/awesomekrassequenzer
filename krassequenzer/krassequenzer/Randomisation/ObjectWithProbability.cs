using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.Randomisation
{
	class ObjectWithProbability<T>
	{
		public T TheObject { get; set; }

		public int Probability { get; set; }

		public ObjectWithProbability(int probability, T theObject)
		{
			this.Probability = probability;
			this.TheObject = theObject;
		}

		/* By not implementing this method we encourage reusing of instances of Random.
		public bool Play()
		{
			return this.Play(new Random());
		}
		 */

		public bool Play(Random rnd)
		{
			return (rnd.Next(100) <= this.Probability);
		}
	}
}

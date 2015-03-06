using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.Randomisation
{

	class RandomUtil // functionable name
	{
		public static T PlayMultiple<T>(IEnumerable<ObjectWithProbability<T>> objectsInGame)
		{
			return PlayMultiple<T>(objectsInGame, new Random());
		}

		public static T PlayMultiple<T>(IEnumerable<ObjectWithProbability<T>> objectsInGame, Random rnd){
			int probsSum = objectsInGame.Sum(x => x.Probability);

			if (probsSum == 0)
			{
				throw new InvalidProbabilityException("The probability of all objects in the game are 0.");
			}

			int pin = rnd.Next(probsSum) + 1;

			int lower = 0;
			int higher = 0;
			foreach(var participant in objectsInGame){
				lower = higher;
				higher += participant.Probability;

				if (lower < pin && pin <= higher)
				{
					return participant.TheObject;
				}
			}

			// something is wrong with the above algorithm
			throw new NotImplementedException();
		}
	}
}

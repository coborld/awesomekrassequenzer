using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.Randomisation
{
	class InvalidProbabilityException : Exception
	{
		public InvalidProbabilityException()
			: base()
		{

		}

		public InvalidProbabilityException(string msg)
			: base(msg)
		{

		}
	}
}

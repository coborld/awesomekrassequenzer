using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MusicModel
{
	class InvalidNoteValueException : Exception
	{
		public InvalidNoteValueException()
		{
		}

		public InvalidNoteValueException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{
		}
	}
}

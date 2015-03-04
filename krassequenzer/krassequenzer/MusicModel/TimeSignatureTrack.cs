using krassequenzer.Stuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MusicModel
{
	public class TimeSignatureTrack
	{
		public TimeSignature StartTimeSignature { get; set; }

		private readonly OrderedCollection<TimeSignatureChange> _changes;

		public ICollection<TimeSignatureChange> TimeSignatureChanges { get { return this._changes; } }
	}
}

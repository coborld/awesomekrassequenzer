using krassequenzer.Stuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MusicModel
{
	/// <summary>
	/// Represents a global track that contains the time signature for each measure
	/// in the score.
	/// </summary>
	public class TimeSignatureTrack
	{
		/// <summary>
		/// Initializes a new instance.
		/// </summary>
		public TimeSignatureTrack()
		{
			this._changes = new OrderedCollection<TimeSignatureChange>((x, y) => x.StartMeasure - y.StartMeasure);
			this.InitialTimeSignature = DefaultInitialTimeSignature;
		}

		/// <summary>
		/// Gets the default value of the <see cref="InitialTimeSignature"/> property
		/// for new instances.
		/// </summary>
		public static readonly TimeSignature DefaultInitialTimeSignature = new TimeSignature(4, 4);

		/// <summary>
		/// Gets the time signature that is valid from the start of the score until the
		/// first time signature change.
		/// </summary>
		public TimeSignature InitialTimeSignature { get; set; }

		private readonly OrderedCollection<TimeSignatureChange> _changes;

		/// <summary>
		/// Gets a collection of time signature changes, each of which represent the
		/// change of time signature at a particular measure.
		/// </summary>
		public ICollection<TimeSignatureChange> TimeSignatureChanges { get { return this._changes; } }
	}
}

using krassequenzer.Stuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MusicModel
{
	/// <summary>
	/// The <see cref="TempoTrack"/> represents a unique, continuous
	/// part of a score and contains tempo changes, that affect all
	/// parts simultaneously.
	/// </summary>
	public class TempoTrack
	{
		public TempoTrack()
		{
			this._tempoChanges = new OrderedCollection<TempoChange>((x, y) => MusicalTime.Comparison(x.Position, y.Position));
		}

		/// <summary>
		/// Gets or sets the initial tempo of the track.
		/// </summary>
		public Tempo InitialTempo { get; set; }

		private readonly OrderedCollection<TempoChange> _tempoChanges;

		/// <summary>
		/// Gets a list containing all tempo changes that come into
		/// effect after the initial tempo.
		/// </summary>
		public ICollection<TempoChange> TempoChanges { get { return this._tempoChanges; } }

		/// <summary>
		/// Gets the interpolated time at the specified <paramref name="time"/>.
		/// </summary>
		public Tempo GetTempoAt(MusicalTime time)
		{
			if (!this.TempoChanges.Any())
			{
				return this.InitialTempo;
			}
			TempoChange previous = null;
			TempoChange next = null;
			foreach (var c in this.TempoChanges)
			{
				if (c.Position == time)
				{
					return c.NewTempo;
				}
				if (c.Position > time)
				{
					next = c;
					break;
				}
				else
				{
					previous = c;
				}
			}
			if (previous == null)
			{
				// the time point we want is between the beginning of the
				// track and 'next'
				if (next.LinearInterpolation)
				{
					var tempoValue = Interpolator.Interp2(time.Ticks, 0, this.InitialTempo.TempoValue, next.Position.Ticks, next.NewTempo.TempoValue);
					return new Tempo(tempoValue);
				}
				else
				{
					return this.InitialTempo;
				}
			}
			if (next == null)
			{
				// the time point we want is after all tempo changes
				// last tempo is valid
				return previous.NewTempo;
			}
			// time is between previous and next
			if (next.LinearInterpolation)
			{
				return previous.NewTempo;
			}
			else
			{
				var tempoValue = Interpolator.Interp2(time.Ticks, previous.Position.Ticks, previous.NewTempo.TempoValue, next.Position.Ticks, next.NewTempo.TempoValue);
				return new Tempo(tempoValue);
			}
		}
	}
}

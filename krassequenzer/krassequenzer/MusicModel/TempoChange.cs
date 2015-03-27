using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MusicModel
{
	/// <summary>
	/// Represents a tempo change event on the tempo track.
	/// Instances of this type are immutable.
	/// </summary>
	public class TempoChange
	{
		/// <summary>
		/// Initializes a new instance.
		/// </summary>
		/// <param name="position">The position at which the tempo change occurs.</param>
		/// <param name="linearInterpolation">Whether to use linear interpolation between
		/// the previous tempo change and this tempo change.</param>
		/// <param name="newTempo">The new tempo that is valid from the position of this
		/// tempo change until the next tempo change.</param>
		public TempoChange(StreamTime position, bool linearInterpolation, Tempo newTempo)
		{
			this._position = position;
			this._linearInterpolation = linearInterpolation;
			this._newTempo = newTempo;
		}

		private readonly StreamTime _position;
		private readonly bool _linearInterpolation;
		private readonly Tempo _newTempo;

		/// <summary>
		/// Gets the position at which the tempo change occurs.
		/// </summary>
		public StreamTime Position { get { return this._position; } }
		/// <summary>
		/// Gets a value indicating whether to use linear interpolation between the
		/// previous tempo change and this.
		/// </summary>
		public bool LinearInterpolation { get { return this._linearInterpolation; } }
		/// <summary>
		/// Gets the new tempo that is valid from the position of this tempo change
		/// until the next tempo change.
		/// </summary>
		public Tempo NewTempo { get { return this._newTempo; } }
	}
}

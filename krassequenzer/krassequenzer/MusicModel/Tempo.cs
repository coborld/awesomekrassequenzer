using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MusicModel
{
	/// <summary>
	/// Represents a musical tempo value.
	/// Instances of this class are immutable.
	/// </summary>
	public sealed class Tempo
	{
		/// <summary>
		/// Initializes a new instance with the specified tempo.
		/// </summary>
		/// <param name="tempo"></param>
		public Tempo(double tempo)
		{
			this._tempo = tempo;
		}

		private readonly double _tempo;
		/// <summary>
		/// Gets the tempo value, in quarter notes per minute.
		/// </summary>
		public double TempoValue { get { return this._tempo; } }
	}
}

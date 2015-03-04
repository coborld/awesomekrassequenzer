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
	public struct Tempo
	{
		/// <summary>
		/// Initializes a new instance with the specified tempo.
		/// </summary>
		/// <param name="tempo"></param>
		public Tempo(double tempo)
		{
			if (tempo <= 0)
			{
				throw new ArgumentOutOfRangeException("Tempo value must be positive.");
			}
			this._tempo = tempo;
		}

		private readonly double _tempo;
		/// <summary>
		/// Gets the tempo value, in quarter notes per minute.
		/// </summary>
		public double TempoValue { get { return this._tempo; } }

		public override int GetHashCode()
		{
			return this._tempo.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Tempo))
			{
				return false;
			}
			var o = (Tempo)obj;
			return o._tempo == this._tempo;
		}

		public static implicit operator Tempo(double d)
		{
			return new Tempo(d);
		}
	}
}

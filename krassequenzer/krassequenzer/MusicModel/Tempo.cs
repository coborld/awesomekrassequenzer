using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using krassequenzer.Stuff;

namespace krassequenzer.MusicModel
{
	/// <summary>
	/// Represents a musical tempo value.
	/// Instances of this class are immutable.
	/// </summary>
	public class Tempo
	{
		/// <summary>
		/// Initializes a new instance with the specified tempo value.
		/// </summary>
		/// <param name="tempoValue">The tempo value, in quarter notes
		/// per minute.</param>
		public Tempo(double tempoValue)
		{
			if (tempoValue <= 0)
			{
				throw new ArgumentOutOfRangeException("Tempo value must be positive.");
			}
			this._tempoValue = tempoValue;
		}

		private readonly double _tempoValue;
		/// <summary>
		/// Gets the tempo value, in quarter notes per minute.
		/// </summary>
		public double TempoValue { get { return this._tempoValue; } }

		public override int GetHashCode()
		{
			return this._tempoValue.GetHashCode();
		}

		/// <summary>
		/// Returns true if the tempo represented by this instance
		/// is equal to <paramref name="obj"/>.
		/// </summary>
		public override bool Equals(object obj)
		{
			if (!(obj is Tempo))
			{
				return false;
			}
			var o = (Tempo)obj;
			return o._tempoValue == this._tempoValue;
		}

		/// <summary>
		/// Implicitly converts a double to a new instance of <see cref="Tempo"/>,
		/// where the double represents the instance's tempo value in 
		/// quarter notes per minute.
		/// </summary>
		public static implicit operator Tempo(double d)
		{
			return new Tempo(d);
		}

		/// <summary>
		/// Returns a string representation containing the tempo value in 
		/// quarter notes per minute.
		/// </summary>
		public override string ToString()
		{
			return this.TempoValue.ToStringIv("F2") + " QPM";
		}
	}
}

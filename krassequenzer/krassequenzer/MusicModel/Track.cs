using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MusicModel
{
	public class Track
	{
		public string Name { get; set; }

		private readonly List<Note> _notes = new List<Note>();
		public List<Note> Notes { get { return this._notes; } }
	}
}
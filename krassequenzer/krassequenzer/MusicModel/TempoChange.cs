using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MusicModel
{
	public class TempoChange
	{
		public MusicalTime Position { get; set; }
		public bool LinearInterpolation { get; set; }
		public Tempo NewTempo { get; set; }
	}
}

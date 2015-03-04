using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.MusicModel
{
    public class Note
    {
        /// <summary>
        /// Eine Viertel-Note.
        /// </summary>
        public const int Quarter = 128;

        public MusicalTime Duration { get; set; }

		public MusicalTime StartPosition { get; set; }

		public int Voice { get; set; }
    }
}

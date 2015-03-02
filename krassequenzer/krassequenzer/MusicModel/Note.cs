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

        public int Duration { get; set; }

        public bool Rest { get; set; }

        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using krassequenzer.MusicModel;

namespace krassequenzer.Views
{
	public partial class NotePropertiesControl : UserControl
	{
		public NotePropertiesControl()
		{
			InitializeComponent();
		}

		private Note _note;
		public Note Note
		{
			get { return this._note; }
			set
			{
				this._note = value;
				this.Rebuild();
			}
		}

		public void Rebuild()
		{

		}
	}
}

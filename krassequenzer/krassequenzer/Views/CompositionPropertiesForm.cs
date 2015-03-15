using krassequenzer.MusicModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace krassequenzer.Views
{
	public partial class CompositionPropertiesForm : Form
	{
		public CompositionPropertiesForm()
		{
			InitializeComponent();
		}

		private ViewContext _context;
		public ViewContext Context
		{
			get { return this._context; }
			set
			{
				this._context = value;
				this.DisplayContent();
			}
		}

		private void DisplayContent()
		{
			if (this.Context == null || this.Context.CurrentComposition.Value == null)
			{
				this.Clear();
				return;
			}
			this.textBoxCompositionTitle.Text = this.Context.CurrentComposition.Value.Title;
		}

		private void Clear()
		{
			this.textBoxCompositionTitle.Text = null;
		}

		private void Commit()
		{
			if (this.Context == null || this.Context.CurrentComposition.Value == null)
			{
				return;
			}
			this.Context.CurrentComposition.Value.Title = this.textBoxCompositionTitle.Text;
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			this.Commit();
			this.Close();
		}

		
	}
}

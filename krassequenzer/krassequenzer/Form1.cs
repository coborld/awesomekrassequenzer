using krassequenzer.MusicModel;
using krassequenzer.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace krassequenzer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
			this.compositionPropertiesFormManager = new ModelessDialogManager(this.CreateCompositionPropertiesForm);
        }

		private readonly ModelessDialogManager compositionPropertiesFormManager;

		private Form CreateCompositionPropertiesForm()
		{
			var form = new CompositionPropertiesForm();
			form.Context = this.Context;
			return form;
		}

		public ViewContext Context { get; set; }

		private void toolStripButtonUpdate_Click(object sender, EventArgs e)
		{

		}

		private void CreateAndLoadNewComposition()
		{
			var composition = new Composition();
			this.Context.CurrentComposition.Value = composition;
		}

		private void toolStripMenuItemCompositionProperties_Click(object sender, EventArgs e)
		{
			this.compositionPropertiesFormManager.Show();
		}

		private void toolStripMenuItemNewComposition_Click(object sender, EventArgs e)
		{
			this.CreateAndLoadNewComposition();
		}
    }
}

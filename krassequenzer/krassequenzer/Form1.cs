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

			// this is the thing that needs to be passed to all other
			// views which need access to the application state
			this.context = new ViewContext();

			this.compositionPropertiesFormManager = new ModelessDialogManager(this.CreateCompositionPropertiesForm);

			this.SetApplicationStatus("Ready");
        }

		private readonly ModelessDialogManager compositionPropertiesFormManager;
		private readonly ViewContext context;
		
		/// <summary>
		/// Gets the invariant <see cref="ViewContext"/> of this application.
		/// </summary>
		public ViewContext Context
		{
			get { return this.context; }
		}

		private void SetApplicationStatus(string message)
		{
			this.toolStripStatusLabelStatus.Text = message;
		}

		private Form CreateCompositionPropertiesForm()
		{
			var form = new CompositionPropertiesForm();
			form.Owner = this;
			form.Context = this.Context;
			return form;
		}

		private void toolStripButtonUpdate_Click(object sender, EventArgs e)
		{
			// do a manual explicit update of all views
			// (no manual updates necessary yet)
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

		private void toolStripMenuItemExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
    }
}

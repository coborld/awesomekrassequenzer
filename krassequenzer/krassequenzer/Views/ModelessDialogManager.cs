using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using krassequenzer.Stuff;

namespace krassequenzer.Views
{
	class ModelessDialogManager
	{
		public ModelessDialogManager(Func<Form> source)
		{
			this.source = source.NotNull("source");
		}

		private readonly Func<Form> source;
		private Form currentForm;

		public void Show()
		{
			if (this.currentForm == null)
			{
				this.currentForm = this.source();
				this.currentForm.FormClosed += this.HandleCurrentFormClosed;
				this.currentForm.Show();
			}
			else
			{
				this.currentForm.Activate();
			}
		}

		public void Close()
		{
			if (this.currentForm != null)
			{
				this.currentForm.Close();
			}
		}

		private void HandleCurrentFormClosed(object sender, EventArgs e)
		{
			this.currentForm.FormClosed -= this.HandleCurrentFormClosed;
			this.currentForm = null;
		}
	}
}

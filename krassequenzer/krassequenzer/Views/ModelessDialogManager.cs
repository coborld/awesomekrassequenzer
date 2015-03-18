using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using krassequenzer.Stuff;
using System.Threading;

namespace krassequenzer.Views
{
	/// <summary>
	/// Each instance of this class makes sure that only exactly one instance
	/// of the form may be open at any time. Forms are created as needed when
	/// they are closed.
	/// Note that the <see cref="Show"/> and <see cref="Close"/> methods must
	/// be called from the thread which created the instance.
	/// </summary>
	class ModelessDialogManager
	{
		/// <summary>
		/// Initializes a new instance.
		/// </summary>
		/// <param name="source">A function returning a new instance of the
		/// form type that this manager should display. The form should not
		/// have had its <see cref="Form.Show"/> method called.</param>
		public ModelessDialogManager(Func<Form> source)
		{
			this.source = source.NotNull("source");
			this.instantiationThread = Thread.CurrentThread;
		}

		/// <summary>
		/// The thread which created this instance is stored to ensure that
		/// the form is running on that thread, which is assumed to be the
		/// UI thread.
		/// </summary>
		private readonly Thread instantiationThread;
		private readonly Func<Form> source;
		private Form currentForm;

		/// <summary>
		/// Shows the form associated with this manager (creates one if
		/// necessary). This method must be called from the thread that
		/// created this instance.
		/// </summary>
		public void Show()
		{
			this.CheckCrossThreadOperation();

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

		/// <summary>
		/// Closes the form associated with this manager if it is currently
		/// shown. Otherwise, no action is taken.
		/// This method must be called from the thread that created this
		/// instance.
		/// </summary>
		public void Close()
		{
			this.CheckCrossThreadOperation();

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

		private void CheckCrossThreadOperation()
		{
			if (Thread.CurrentThread != this.instantiationThread)
			{
				throw new InvalidOperationException(this.GetType().Name + " was used from another thread than the thread it was created on.");
			}
		}
	}
}

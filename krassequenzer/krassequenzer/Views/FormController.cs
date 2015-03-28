using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using krassequenzer.Stuff;

namespace krassequenzer.Views
{
	internal class FormController
	{
		public FormController(ViewContext context, Form owner)
		{
			this._context = context.NotNull("context");
			this._owner = owner;

			this.compositionPropertiesFormManager = new ModelessDialogManager(this._owner, this.CreateCompositionPropertiesForm);
			this.deviceSetupFormManager = new ModelessDialogManager(this._owner, this.CreateDeviceSetupForm);
			this.objectPropertiesFormManager = new ModelessDialogManager(this._owner, this.CreateObjectPropertiesForm);
		}

		private readonly ViewContext _context;
		/// <summary>
		/// Gets the <see cref="ViewContext"/> of the application, which is the root
		/// element of the application model.
		/// </summary>
		public ViewContext Context { get { return this._context; } }

		/// <summary>
		/// Gets the owner window that is passed to all <see cref="ModelessDialogManager"/> controlled forms.
		/// </summary>
		private readonly Form _owner;

		private readonly ModelessDialogManager compositionPropertiesFormManager;
		private readonly ModelessDialogManager deviceSetupFormManager;
		private readonly ModelessDialogManager objectPropertiesFormManager;

		/// <summary>
		/// Gets a <see cref="ModelessDialogManager"/> that can be used to display composition properties.
		/// </summary>
		public ModelessDialogManager CompositionPropertiesFormManager { get { return this.compositionPropertiesFormManager; } }
		/// <summary>
		/// Gets a <see cref="ModelessDialogManager"/> that can be used to display the hardware device setup window.
		/// </summary>
		public ModelessDialogManager DeviceSetupFormManager { get { return this.deviceSetupFormManager; } }
		/// <summary>
		/// Gets a <see cref="ModelessDialogManager"/> that can be used to display an object properties window.
		/// </summary>
		public ModelessDialogManager ObjectPropertiesFormManager { get { return this.objectPropertiesFormManager; } }

		private Form CreateCompositionPropertiesForm()
		{
			var form = new CompositionPropertiesForm();
			form.Context = this.Context;
			return form;
		}

		private Form CreateDeviceSetupForm()
		{
			var form = new DeviceSetupForm();
			form.Context = this.Context;
			return form;
		}

		private Form CreateObjectPropertiesForm()
		{
			var form = new ObjectPropertiesForm();
			form.Context = this.Context;
			return form;
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using krassequenzer.Stuff;
using krassequenzer.MusicModel;

namespace krassequenzer.Views
{
	public partial class ObjectPropertiesForm : Form
	{
		public ObjectPropertiesForm()
		{
			InitializeComponent();
			this._context = new ObservingProperty<ViewContext, object>(x => x.SelectedObject, this.HandleSelectedObjectChanged);

			var notePropertiesControl = new NotePropertiesControl();
			this.panelControlContainer.Controls.Add(notePropertiesControl);

			// add each control with its associated type into the dictionary so that we can decide which control to use
			// when the selected object changes
			this.controlsByType = new Dictionary<Type, Func<object, Control>>();
			this.controlsByType.Add(typeof(Note), x => { notePropertiesControl.Note = (Note)x; return notePropertiesControl; });
		}

		/// <summary>
		/// Contains a control for each possible type that is editable by this form.
		/// </summary>
		private readonly Dictionary<Type, Func<object, Control>> controlsByType;

		private readonly ObservingProperty<ViewContext, object> _context;
		/// <summary>
		/// Gets or sets the <see cref="ViewContext"/> whose <see cref="ViewContext.SelectedObject"/> property is being
		/// displayed for editing in this instance.
		/// </summary>
		public ViewContext Context
		{
			get { return this._context.Context; }
			set { this._context.Context = value; }
		}

		private void HandleSelectedObjectChanged(object oldValue, object newValue)
		{
			// make each control invisble, so that only the relevant control is shown later
			foreach (Control control in this.panelControlContainer.Controls)
			{
				control.Visible = false;
			}
			
			Func<object, Control> controlAcquirer;
			var success = this.controlsByType.TryGetValue(newValue.GetType(), out controlAcquirer);
			if (!success)
			{
				// unknown object type - cannot display
			}
			var newControl = controlAcquirer(newValue);
			newControl.Visible = true;
		}
	}
}

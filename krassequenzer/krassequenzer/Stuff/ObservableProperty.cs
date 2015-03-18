using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.Stuff
{
	/// <summary>
	/// Represents a property whose value can change.
	/// When the value changes, an event is raised.
	/// Instances of this class are intended to be treated as invariants.
	/// Mutating the <see cref="Value"/> does not raise an event.
	/// </summary>
	public sealed class ObservableProperty<T>
	{
		private T _value;

		/// <summary>
		/// Gets or sets the value of the property.
		/// Changing the value will call event subscribers of this
		/// instance.
		/// </summary>
		public T Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if (Object.ReferenceEquals(this._value, value))
				{
					return;
				}
				if (this._value != null)
				{
					this.OnDetachCue();
				}
				this._value = value;
				if (this._value != null)
				{
					this.OnAttachCue();
				}
				this.OnValueChanged();
			}
		}

		/// <summary>
		/// Occurs when the value of this instance changes.
		/// </summary>
		public event EventHandler ValueChanged;

		/// <summary>
		/// Occurs after the value of this instance changes if it is
		/// not null.
		/// Event subscribers can safely attach events to the object in
		/// <see cref="Value"/>.
		/// </summary>
		public event EventHandler AttachCue;
		/// <summary>
		/// Occurs before the value of this instance changes if it is
		/// not null.
		/// Event subscribers can safely detach events from the object
		/// in <see cref="Value"/>.
		/// </summary>
		public event EventHandler DetachCue;

		private void OnAttachCue()
		{
			var handler = this.AttachCue;
			if (handler != null)
			{
				handler(this, EventArgs.Empty);
			}
		}

		private void OnDetachCue()
		{
			var handler = this.DetachCue;
			if (handler != null)
			{
				handler(this, EventArgs.Empty);
			}
		}

		private void OnValueChanged()
		{
			var handler = this.ValueChanged;
			if (handler != null)
			{
				handler(this, EventArgs.Empty);
			}
		}
	}
}

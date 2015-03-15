using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.Stuff
{
	public sealed class ObservableProperty<T>
	{
		private T _value;

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

		public EventHandler ValueChanged { get; set; }

		public EventHandler AttachCue { get; set; }
		public EventHandler DetachCue { get; set; }

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

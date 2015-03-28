using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.Stuff
{
	internal class ObservingProperty<TContext, TValue>
	{
		public ObservingProperty(Func<TContext, ObservableProperty<TValue>> observablePropertySelector, Action<TValue, TValue> changedHandler)
		{
			this._observablePropertySelector = observablePropertySelector.NotNull("observableProperty");
			this._changedHandler = changedHandler.NotNull("changedHandler");
		}

		private readonly Func<TContext, ObservableProperty<TValue>> _observablePropertySelector;
		private readonly Action<TValue, TValue> _changedHandler;

		private TContext _context;

		public TContext Context
		{
			get { return this._context; }
			set
			{
				this._context.Maybe(x => this._observablePropertySelector(x).ValueChanged -= this.HandleObservablePropertyValueChanged);
				var oldValue = this._context.Maybe(x => this._observablePropertySelector(x).Value);
				this._context = value;
				var newValue = this._context.Maybe(x => this._observablePropertySelector(x).Value);
				this._changedHandler(oldValue, newValue);
				this._context.Maybe(x => this._observablePropertySelector(x).ValueChanged += this.HandleObservablePropertyValueChanged);
			}
		}

		private void HandleObservablePropertyValueChanged(object sender, ObservablePropertyValueChangedEventArgs<TValue> e)
		{
			this._changedHandler(e.OldValue, e.NewValue);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace krassequenzer.Views
{
	internal class SensibleScrollBar
	{
		public SensibleScrollBar(ScrollBar scrollBar)
		{
			if (scrollBar == null) throw new ArgumentNullException("scrollBar");
			this.scrollBar = scrollBar;
			this.scrollBar.Minimum = 0;

			this._maximum = this.scrollBar.Maximum - this.LargeChange + 1;
			this._largeChange = this.scrollBar.LargeChange;

			this.scrollBar.ValueChanged += this.HandleValueChanged;
		}

		public void Close()
		{
			this.scrollBar.ValueChanged -= this.HandleValueChanged;
		}

		private readonly ScrollBar scrollBar;

		private void UpdateScrollBar()
		{
			// ???
			this.scrollBar.Maximum = this.Maximum + this.LargeChange - 1;
			this.scrollBar.LargeChange = this.LargeChange;
		}

		private int _maximum;
		public int Maximum
		{
			get { return _maximum; }
			set
			{
				this.scrollBar.Enabled = value > 0;
				this._maximum = value;
				this.UpdateScrollBar();
			}
		}
		public int Value
		{
			get { return this.scrollBar.Value; }
			set { this.scrollBar.Value = value; }
		}
		public int SmallChange
		{
			get { return this.scrollBar.SmallChange; }
			set { this.scrollBar.SmallChange = value; }
		}
		private int _largeChange;
		public int LargeChange
		{
			get { return this._largeChange; }
			set
			{

				this._largeChange = value;
				this.UpdateScrollBar();
			}
		}

		public event EventHandler ValueChanged;

		protected virtual void OnValueChanged(EventArgs e)
		{
			var handler = this.ValueChanged;
			if (handler != null)
			{
				handler(this, e);
			}
		}

		private void HandleValueChanged(object sender, EventArgs e)
		{
			this.OnValueChanged(EventArgs.Empty);
		}
	}
}

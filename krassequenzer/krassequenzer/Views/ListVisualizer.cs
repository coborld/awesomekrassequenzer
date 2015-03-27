using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Diagnostics;

namespace krassequenzer.Views
{
	internal class ListVisualizer<TContent, TControl>
		where TControl : Control
	{
		public ListVisualizer(Control containerControl, Action<TControl, TContent> contentSetter, Func<TControl> contentControlSource, SensibleScrollBar scrollBar)
		{
			if (contentSetter == null) throw new ArgumentNullException("contentSetter");
			if (contentControlSource == null) throw new ArgumentNullException("contentControlSource");
			this.contentSetter = contentSetter;
			this.container = new ContainerController(containerControl);
			this.contentControlSource = contentControlSource;
			this.scroller = new Scroller(this, scrollBar);
		}

		private readonly Func<TControl> contentControlSource;
		private readonly Action<TControl, TContent> contentSetter;
		private readonly ContainerController container;
		private readonly Scroller scroller;

		private class ContainerController
		{
			public ContainerController(Control containerControl)
			{
				if (containerControl == null) throw new ArgumentNullException("containerControl");
				this._containerControl = containerControl;
				this.controls = new List<TControl>();
			}

			private readonly Control _containerControl;
			private readonly List<TControl> controls;

			public Control ContainerControl { get { return this._containerControl; } }

			public void Add(TControl control)
			{
				this.ContainerControl.Controls.Add(control);
				this.controls.Add(control);
			}

			public void RemoveAt(int index)
			{
				var control = this.controls[index];
				this.controls.RemoveAt(index);
				this.ContainerControl.Controls.Remove(control);
			}

			public TControl this[int index]
			{
				get { return this.controls[index]; }
			}

			public int Count { get { return this.controls.Count; } }

			public void Clear()
			{
				this.controls.Clear();
				this.ContainerControl.Controls.Clear();
			}
		}

		private class Scroller
		{
			public Scroller(ListVisualizer<TContent, TControl> visualizer, SensibleScrollBar scrollBar)
			{
				if (visualizer == null) throw new ArgumentNullException("visualizer");
				if (scrollBar == null) throw new ArgumentNullException("scrollBar");
				this.visualizer = visualizer;
				this.scrollBar = scrollBar;

				this.scrollBar.ValueChanged += this.HandleScrollBarScroll;
			}

			private readonly ListVisualizer<TContent, TControl> visualizer;
			private readonly SensibleScrollBar scrollBar;

			public void Update(int totalItems)
			{
				this.scrollBar.Maximum = totalItems - 1;
			}

			private void HandleScrollBarScroll(object sender, EventArgs e)
			{
				this.visualizer.ScrollPosition = this.scrollBar.Value;
			}
		}

		private IList<TContent> _content;
		public IList<TContent> Content
		{
			get { return _content; }
			set
			{
				this._content = value;
				this.UpdateContent();
			}
		}

		public event EventHandler ContentUpdated;
		protected virtual void OnContentUpdated(EventArgs e)
		{
			var handler = this.ContentUpdated;
			if (handler != null) handler(this, e);
		}

		private int _scrollPosition;
		public int ScrollPosition
		{
			get { return this._scrollPosition; }
			set
			{
				this._scrollPosition = value;
				this.UpdateContent();
			}
		}

		public void UpdateContent()
		{
			if (this.Content == null || this.Content.Count == 0)
			{
				this.NumVisibleControls = 0;
				this.container.Clear();
				return;
			}
			if (this.ScrollPosition >= this.Content.Count)
			{
				this.ScrollPosition = this.Content.Count - 1;
			}
			this.UpdateContentInternal();
			this.OnContentUpdated(EventArgs.Empty);
		}

		public int NumVisibleControls { get; private set; }

		private void UpdateContentInternal()
		{
			int controlIndex = 0;
			int top = 0;

			// Set the content of all controls.
			for (int i = this.ScrollPosition; i < this.Content.Count; ++i)
			{
				var contentElement = this.Content[i];
				// Add controls if necessary
				while (this.container.Count <= controlIndex)
				{
					var newControl = this.CreateContentControl();
					this.container.Add(newControl);
				}
				var control = this.container[controlIndex];
				this.contentSetter(control, contentElement);
				control.Top = top;
				top += control.Height;
				controlIndex += 1;

				// We have filled the entire visible area - don't need
				// more controls.
				if (top >= this.container.ContainerControl.Height)
				{
					break;
				}
			}

			this.scroller.Update(this.Content.Count);

			this.NumVisibleControls = controlIndex;

			// Remove excess controls.
			while (controlIndex < this.container.Count)
			{
				this.container.RemoveAt(controlIndex);
			}
		}

		private TControl CreateContentControl()
		{
			return this.contentControlSource();
		}
	}
}

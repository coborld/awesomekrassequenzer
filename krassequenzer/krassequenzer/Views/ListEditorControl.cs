using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using krassequenzer.MusicModel;

using krassequenzer.Stuff;

namespace krassequenzer.Views
{
	public partial class ListEditorControl : UserControl
	{
		public ListEditorControl()
		{
			InitializeComponent();

			this.comboBoxEventDisplay.Items.Add(new EventDisplayComboBoxItem(new NoteEventDisplayMode(this)));
			this.comboBoxEventDisplay.SelectedIndex = 0;

			this.comboBoxEventDisplay.SelectedIndexChanged += this.HandleComboBoxEventDisplaySelectedIndexChanged;
		}

		private class EventDisplayComboBoxItem
		{
			public EventDisplayComboBoxItem(IEventDisplayMode eventDisplayMode)
			{
				this._displayMode = eventDisplayMode.NotNull("eventDisplayMode");
			}

			private readonly IEventDisplayMode _displayMode;
			public IEventDisplayMode DisplayMode { get { return this._displayMode; } }

			public override string ToString()
			{
				return this.DisplayMode.Name;
			}
		}

		private interface IEventDisplayMode
		{
			string Name { get; }
			IEnumerable<ListViewItem> CreateItems(Track t);
		}

		private class NoteEventDisplayMode : IEventDisplayMode
		{
			public NoteEventDisplayMode(ListEditorControl control)
			{
				this.control = control.NotNull("control");
			}

			private readonly ListEditorControl control;

			public string Name
			{
				get { return "Notes"; }
			}

			public IEnumerable<ListViewItem> CreateItems(Track t)
			{
				return t.NotNull("t").Notes.Select(x => ConvertNote(x));
			}

			private static ListViewItem ConvertNote(Note n)
			{
				var lvi = new ListViewItem(n.ScoreStartPosition.ToString());
				lvi.Tag = n;
				var sb = new StringBuilder();
				sb.Append("D: " + n.ScoreDuration);
				sb.Append("; ");
				lvi.SubItems.Add(sb.ToString());
				return lvi;
			}
		}

		/// <summary>
		/// Gets or sets an action that is called when the selected object changed.
		/// </summary>
		public Action<object> ObjectPropertiesCaller { get; set; }

		private Track track;

		public Track Track
		{
			get { return this.track; }
			set
			{
				this.track = value;
				this.Rebuild();
			}
		}

		private IEventDisplayMode ActiveDisplayMode
		{
			get { return ((EventDisplayComboBoxItem)this.comboBoxEventDisplay.SelectedItem).DisplayMode; }
		}

		public void Rebuild()
		{
			this.listViewEventList.Items.Clear();
			if (this.track == null)
			{
				return;
			}

			foreach (var item in this.ActiveDisplayMode.CreateItems(this.track))
			{
				this.listViewEventList.Items.Add(item);
			}
		}

		private void HandleComboBoxEventDisplaySelectedIndexChanged(object sender, EventArgs e)
		{
			this.Rebuild();
		}
	}
}

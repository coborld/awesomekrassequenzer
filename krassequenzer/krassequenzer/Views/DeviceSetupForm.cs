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
using krassequenzer.MidiPlayback;

namespace krassequenzer.Views
{
	public partial class DeviceSetupForm : Form
	{
		public DeviceSetupForm()
		{
			InitializeComponent();

			this.updating = true;
			try
			{
				this.listViewMidiOutputInterfaces.Items.Clear();

				// we need to query the hardware interfaces now
				var info = MidiSystemInfo.Query();
				foreach (var outputDeviceInfo in info.OutDeviceInfo)
				{
					var item = new ListViewItem(outputDeviceInfo.Name);
					item.Tag = outputDeviceInfo;
					item.SubItems.Add(outputDeviceInfo.Id.ToString());
					this.listViewMidiOutputInterfaces.Items.Add(item);
				}
			}
			finally
			{
				this.updating = false;
			}
		}

		private ViewContext _context;
		public ViewContext Context
		{
			get { return this._context; }
			set
			{
				this._context.Maybe(x => x.DeviceSetup.ValueChanged -= this.HandleDeviceSetupChanged);
				this._context = value;
				this.Rebuild();
				this._context.Maybe(x => x.DeviceSetup.ValueChanged += this.HandleDeviceSetupChanged);
			}
		}

		private void HandleDeviceSetupChanged(object sender, EventArgs e)
		{
			this.Rebuild();
		}

		private bool updating;

		public void Rebuild()
		{
			this.updating = true;
			try
			{
				var selectedDeviceId = this._context.Maybe(x => x.DeviceSetup.Value).Maybe(x => x.MidiOutputDeviceId);
				foreach (ListViewItem item in this.listViewMidiOutputInterfaces.Items)
				{
					var deviceInfo = (MidiOutDeviceInfo)item.Tag;
					if (deviceInfo.Id == selectedDeviceId)
					{
						item.Selected = true;
						break;
					}
				}
			}
			finally
			{
				this.updating = false;
			}
		}

		private void listViewMidiOutputInterfaces_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.updating)
			{
				return;
			}
			if (this.listViewMidiOutputInterfaces.SelectedItems.Count != 1)
			{
				return;
			}
			var selectedItem = this.listViewMidiOutputInterfaces.SelectedItems[0];
			var selectedMidiOutDeviceInfo = (MidiOutDeviceInfo)selectedItem.Tag;
			this.Context.Maybe(x => x.DeviceSetup.Value = new DeviceSettings.DeviceSetup((int)selectedMidiOutDeviceInfo.Id));
		}
	}
}

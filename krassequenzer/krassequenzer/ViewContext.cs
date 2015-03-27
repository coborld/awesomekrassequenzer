using krassequenzer.DeviceSettings;
using krassequenzer.MusicModel;
using krassequenzer.Stuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer
{
	/// <summary>
	/// Instances of this class represent a unique, invariant application state
	/// container.
	/// The same instance of this class should be passed to all views, ensuring that
	/// each view has access to the same global state information, such as the
	/// currently active <see cref="Composition"/>.
	/// </summary>
	public class ViewContext
	{
		private readonly ObservableProperty<Composition> _currentComposition = new ObservableProperty<Composition>();
		/// <summary>
		/// Gets or sets the composition that is currently active in the application.
		/// </summary>
		public ObservableProperty<Composition> CurrentComposition { get { return this._currentComposition; } }

		private readonly ObservableProperty<DeviceSetup> _deviceSetup = new ObservableProperty<DeviceSetup>();
		/// <summary>
		/// Gets or sets the currently active <see cref="DeviceSetup"/>.
		/// </summary>
		public ObservableProperty<DeviceSetup> DeviceSetup { get { return this._deviceSetup; } }
	}
}

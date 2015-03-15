using krassequenzer.MusicModel;
using krassequenzer.Stuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer
{
	public class ViewContext
	{
		private readonly ObservableProperty<Composition> _currentComposition = new ObservableProperty<Composition>();
		public ObservableProperty<Composition> CurrentComposition { get { return this._currentComposition; } }
	}
}

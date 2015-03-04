using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using krassequenzer.MusicModel;

namespace TheUnitTest
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			var score = new Score();
			var tt = score.TempoTrack;
			tt.InitialTempo = new Tempo(20);
			tt.TempoChanges.Add(new TempoChange(new MusicalTime(100), false, 40));
			tt.TempoChanges.Add(new TempoChange(new MusicalTime(300), false, 60));

			// no linear interpolation
			{
				var t50 = tt.GetTempoAt(new MusicalTime(50));
				var t200 = tt.GetTempoAt(new MusicalTime(200));

				Assert.AreEqual(new Tempo(20), t50);
				Assert.AreEqual(new Tempo(40), t200);
			}

			tt.TempoChanges.Clear();

			tt.TempoChanges.Add(new TempoChange(new MusicalTime(100), true, 40));
			tt.TempoChanges.Add(new TempoChange(new MusicalTime(300), true, 60));

			// with linear interpolation
			{
				var t50 = tt.GetTempoAt(new MusicalTime(50));
				var t200 = tt.GetTempoAt(new MusicalTime(200));

				Assert.AreEqual(new Tempo(30), t50);
				Assert.AreEqual(new Tempo(50), t200);
			}
		}
	}
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using krassequenzer.MusicModel;
using krassequenzer.MidiPlayback;
using System.Collections.Generic;
using System.Linq;

namespace KrassequenzerTester
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			var score = new Composition();
			var tt = score.TempoTrack;
			tt.InitialTempo = new Tempo(20);
			tt.TempoChanges.Add(new TempoChange(new StreamTime(100), false, 40));
			tt.TempoChanges.Add(new TempoChange(new StreamTime(300), false, 60));

			// no linear interpolation
			{
				var t50 = tt.GetTempoAt(new StreamTime(50));
				var t200 = tt.GetTempoAt(new StreamTime(200));

				Assert.AreEqual(new Tempo(20), t50);
				Assert.AreEqual(new Tempo(40), t200);
			}

			tt.TempoChanges.Clear();

			tt.TempoChanges.Add(new TempoChange(new StreamTime(100), true, 40));
			tt.TempoChanges.Add(new TempoChange(new StreamTime(300), true, 60));

			// with linear interpolation
			{
				var t50 = tt.GetTempoAt(new StreamTime(50));
				var t200 = tt.GetTempoAt(new StreamTime(200));

				Assert.AreEqual(new Tempo(30), t50);
				Assert.AreEqual(new Tempo(50), t200);
			}
		}

		[TestMethod]
		public void MergerTest()
		{
			var t0 = new List<IMidiStreamEvent>();
			t0.Add(new MidiStreamEvent(0, 0));
			t0.Add(new MidiStreamEvent(20, 2));
			t0.Add(new MidiStreamEvent(21, 4));

			var t1 = new List<IMidiStreamEvent>();
			t1.Add(new MidiStreamEvent(0, 1));
			t1.Add(new MidiStreamEvent(25, 3));

			var tracks = (new[] { t0, t1 }).Select(x => x.AsEnumerable());

			var merged = MidiStreamEventMerger.Merge(tracks);

			// verify that the events are now in the correct order, as specified
			// by the data property of the events
			Assert.IsTrue(Enumerable.Range(0, 5).SequenceEqual(merged.Select(x => (int)((MidiStreamEvent)x).Data)));

			// verify that the delta times are correct
			var expectedDeltaTimes = new [] { 0, 0, 20, 5, 16 };
			Assert.IsTrue(expectedDeltaTimes.SequenceEqual(merged.Select(x => (int)x.DeltaTime)));
		}
	}
}

﻿using krassequenzer;
using krassequenzer.GenerationStuff;
using krassequenzer.MusicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrassequenzerTester
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			new PinoPlaybackTest().createMidiDemo();
			//ParseTheShit.Run();
			//MidiPlaybackTest.Run();

#if false
			var test = new AwesomeGenerator();
			test.NrNotesToGenerate = 10;

			var part = new Track();

			test.Generate(part);

			Console.Write(part.ToString());
			Console.ReadLine();
#endif
		}

		static void CreateTestScore()
		{
			var score = new Composition();

			score.Title = "test";

			var vTrack = new Track();
			vTrack.Name = "v";
			var q2 = StreamTime.TicksPerQuarter / 2;
			var q = StreamTime.TicksPerQuarter;
			var qd = q + q2;

			int startPosition = 0;

			Func<int, Note> vAdder = x =>
				{
					var n = new Note() { StartPositionOffset = new StreamTime(startPosition), DurationOffset = new StreamTime(x) };
					vTrack.Notes.Add(n);
					startPosition += x;
					return n;
				};
			vAdder(q2);
			vAdder(q2);
			vAdder(q);
			vAdder(q2);
			vAdder(q2);
			vAdder(qd);
			vAdder(q2);
			vAdder(q2);
			vAdder(q2);
			vAdder(q);

			startPosition += q;

			vAdder(q2);
			vAdder(q2);
			vAdder(q);
			vAdder(q2);
			vAdder(q2);
			vAdder(q);
			vAdder(q);
			vAdder(q * 4);
		}
	}
}

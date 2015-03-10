using krassequenzer.GenerationStuff;
using krassequenzer.MusicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrassequenzerTester
{
	public class AwesomeGeneratorTest
	{

		public static void generateAndOutputAsString()
		{
			AwesomeGenerator gen = new AwesomeGenerator() { NrNotesToGenerate = 10 };
			Track track = new Track();
			track.Name = "Composed by AwesomeGenerator";
			gen.Generate(track);

			System.Console.WriteLine(track);
			System.Console.ReadLine();
		}


	}
}

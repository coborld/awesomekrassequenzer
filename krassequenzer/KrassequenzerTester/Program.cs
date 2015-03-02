using krassequenzer;
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
		static void Main(string[] args)
		{
			var test = new AwesomeGenerator();
			test.MaxDuration = 8 * Note.Quarter;
			test.NrNotesToGenerate = 10;

			var part = new Part();

			test.Generate(part);

			Console.Write(part.ToString());
			Console.ReadLine();
		}
	}
}

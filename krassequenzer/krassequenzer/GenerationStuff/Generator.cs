﻿using krassequenzer.MusicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krassequenzer.GenerationStuff
{
	interface Generator
	{
		List<Note> Generate();
	}
}

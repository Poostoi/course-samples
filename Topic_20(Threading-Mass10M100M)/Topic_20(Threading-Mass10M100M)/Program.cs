﻿using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Topic_20_Threading_Mass10M100M_
{
	class Program
	{		
		static void Main(string[] args)
		{
			int[] mass10M =  new int[10000000];
			int[] mass100M = new int[100000000];	

			var generationmass = new GenerationMass();
			
			generationmass.SerialCalc(mass100M);			
			generationmass.ParallelCalc(mass100M);
						
		}
		
	}
}

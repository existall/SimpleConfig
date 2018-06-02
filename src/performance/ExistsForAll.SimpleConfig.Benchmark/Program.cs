using System;
using BenchmarkDotNet.Running;

namespace ExistsForAll.SimpleConfig.Benchmark
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			//var results = new SimpleConfigBenchmark().Run();

			var results = BenchmarkRunner.Run(typeof(SimpleConfigBenchmark));
		}
	}
}
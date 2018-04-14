using BenchmarkDotNet.Running;

namespace ExistsForAll.SimpleConfig.Benchmark
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			new SimpleConfigBenchmark().Run();

			//BenchmarkRunner.Run(typeof(SimpleConfigBenchmark));
		}
	}
}
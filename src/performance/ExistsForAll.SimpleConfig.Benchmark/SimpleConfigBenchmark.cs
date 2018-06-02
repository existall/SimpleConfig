using System.Linq;
using BenchmarkDotNet.Attributes;
using ExistAll.SimpleConfig;

namespace ExistsForAll.SimpleConfig.Benchmark
{
	public class SimpleConfigBenchmark
	{
		[Benchmark]
		public void Run()
		{
			var configBuilder = new ConfigBuilder();
			var configCollection = configBuilder.Build(GetType().Assembly);
			var count = configCollection.Count();
		}
	}
}

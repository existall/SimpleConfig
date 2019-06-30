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
            var configBuilder = ConfigBuilder.CreateBuilder();
            var configCollection = configBuilder.ScanAssemblies(GetType().Assembly);
			var count = configCollection.Count();
		}
	}
}

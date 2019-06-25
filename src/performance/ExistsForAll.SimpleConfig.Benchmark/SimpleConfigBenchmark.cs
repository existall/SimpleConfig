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
			var configBuilder = ConfigBuilder.CreateBuilder()
				.AddAssembly(GetType().Assembly);
			var configCollection = configBuilder.Build();
			var count = configCollection.Count();
		}
	}
}

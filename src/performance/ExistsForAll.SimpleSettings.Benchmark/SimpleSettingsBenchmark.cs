using System.Linq;
using BenchmarkDotNet.Attributes;
using ExistAll.SimpleSettings;

namespace ExistsForAll.SimpleConfig.Benchmark
{
	public class SimpleSettingsBenchmark
	{
		[Benchmark]
		public void Run()
        {
            var settingsBuilder = SettingsBuilder.CreateBuilder();
            var settingsCollection = settingsBuilder.ScanAssemblies(GetType().Assembly);
			var count = settingsCollection.Count();
		}
	}
}

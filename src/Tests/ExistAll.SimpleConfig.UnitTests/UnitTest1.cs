using System;
using System.Reflection;
using ExistAll.SimpleConfig.Core.Reflection;
using Xunit;

namespace ExistAll.SimpleConfig.UnitTests
{
	public class UnitTest1
	{
		[Fact]
		public void Test1()
		{
			ConfigClassGenerator generator = new ConfigClassGenerator();
			var generateType = generator.GenerateType(typeof(IX1));
			var generateType1 = generator.GenerateType(typeof(IX2));

			var instance = (IX1)Activator.CreateInstance(generateType);
		}

		[Fact]
		public void Test2()
		{
			var collection = new AssemblyCollection()
				.AddFullAssemblyHolder(this.GetType().GetTypeInfo().Assembly);

			var t = new ConfigBuilder().Build(collection, new ConfigOptions());

			var config = t.GetConfig<IX1>();
		}
	}


	public interface IX1 : IConfigSection
	{
		string Name { get; set; }
	}

	[ConfigSection]
	public interface IX2
	{
		[DefaultValue(4)]
		int Number { get; set; }
	}
}

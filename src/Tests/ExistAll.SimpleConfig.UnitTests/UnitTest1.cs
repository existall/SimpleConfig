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

			var instance = (IX1) Activator.CreateInstance(generateType);
		}

		[Fact]
		public void Test2()
		{
			var configCollection = new ConfigBuilder().Build(new[] {GetType().GetTypeInfo().Assembly}, new ConfigOptions());

			var config = configCollection.GetConfig<IX1>();

			foreach (var configItem in configCollection)
			{
				Type interfaceType = configItem.Key;
				object configImplemintation = configItem.Value;
			}
		}
	}
}
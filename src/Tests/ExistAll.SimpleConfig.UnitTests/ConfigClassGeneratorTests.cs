using System;
using System.Reflection;
using ExistAll.SimpleConfig.Core.Reflection;
using Xunit;

namespace ExistAll.SimpleConfig.UnitTests
{
	public class ConfigClassGeneratorTests
	{
		[Fact]
		public void GenerateType_WhenGivenAnInterface_ShouldCreateType()
		{
			var generator = new ConfigClassGenerator();

			var type = typeof(IInterfaceOne);

			var result = generator.GenerateType(type);

			var instance = (IInterfaceOne) Activator.CreateInstance(result);

			var isAssignableFrom = type.IsInstanceOfType(instance);

			Assert.True(isAssignableFrom);
		}

		[Fact]
		public void Test2()
		{
			var configCollection = new ConfigBuilder().Build(new[] {GetType().GetTypeInfo().Assembly}, new ConfigOptions());

			var config = configCollection.GetConfig<IInterfaceOne>();

			foreach (var configItem in configCollection)
			{
				Type interfaceType = configItem.Key;
				object configImplemintation = configItem.Value;
			}
		}
	}
}
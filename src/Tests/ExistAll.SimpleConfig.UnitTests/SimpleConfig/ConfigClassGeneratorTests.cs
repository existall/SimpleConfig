using System;
using System.Reflection;
using ExistAll.SimpleConfig.Core.Reflection;
using Xunit;

namespace ExistAll.SimpleConfig.UnitTests.SimpleConfig
{
	public class ConfigClassGeneratorTests
	{
		[Fact]
		public void GenerateType_WhereTypeCreated_ShouldDerivedFromInterface()
		{
			var interfaceType = typeof(ITestInterface);

			var sut = new ConfigClassGenerator();

			var result = sut.GenerateType(interfaceType);

			var typeInfo = result.GetTypeInfo();

			Assert.True(typeInfo.IsClass);
			Assert.True(interfaceType.GetTypeInfo().IsAssignableFrom(typeInfo));
		}

		[Fact]
		public void GenerateType_WhenGivenAnInterface_ShouldCreateType()
		{
			var generator = new ConfigClassGenerator();

			var type = typeof(IInterfaceOne);

			var result = generator.GenerateType(type);

			var instance = (IInterfaceOne)Activator.CreateInstance(result);

			var isAssignableFrom = type.IsInstanceOfType(instance);

			Assert.True(isAssignableFrom);
		}

		[Fact]
		public void GenerateType_WhenGivenAnInterfaceInheritance_ShouldCreateTypeFromDerived()
		{
			var generator = new ConfigClassGenerator();

			var type = typeof(IInterfaceOneChild);

			var result = generator.GenerateType(type);

			var instance = (IInterfaceOneChild)Activator.CreateInstance(result);

			var isAssignableFrom = type.IsInstanceOfType(instance);

			Assert.NotNull(result.GetProperty(nameof(IInterfaceOneChild.Age)));
			Assert.NotNull(result.GetProperty(nameof(IInterfaceOneChild.Name)));
			Assert.True(isAssignableFrom);
		}
	}
}

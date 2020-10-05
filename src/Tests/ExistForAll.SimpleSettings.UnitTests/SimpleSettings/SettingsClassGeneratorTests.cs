using System;
using System.Reflection;
using ExistForAll.SimpleSettings.Core.Reflection;
using Xunit;

namespace ExistForAll.SimpleSettings.UnitTests.SimpleSettings
{
	public class SettingsClassGeneratorTests
	{
		[Fact]
		public void GenerateType_WhereTypeCreated_ShouldDerivedFromInterface()
		{
			var interfaceType = typeof(ITestInterface);

			var sut = new SettingsClassGenerator();

			var result = sut.GenerateType(interfaceType);

			var typeInfo = result.GetTypeInfo();

			Assert.True(typeInfo.IsClass);
			Assert.True(interfaceType.GetTypeInfo().IsAssignableFrom(typeInfo));
		}

		[Fact]
		public void GenerateType_WhenGivenAnInterface_ShouldCreateType()
		{
			var generator = new SettingsClassGenerator();

			var type = typeof(IRoot);

			var result = generator.GenerateType(type);

			var instance = (IRoot)Activator.CreateInstance(result);

			var isAssignableFrom = type.IsInstanceOfType(instance);

			Assert.True(isAssignableFrom);
		}

		[Fact]
		public void GenerateType_WhenGivenAnInterfaceInheritance_ShouldCreateTypeFromDerived()
		{
			var generator = new SettingsClassGenerator();

			var type = typeof(IRootChild);

			var result = generator.GenerateType(type);

			var instance = (IRootChild)Activator.CreateInstance(result);

			var isAssignableFrom = type.IsInstanceOfType(instance);

			Assert.NotNull(result.GetProperty(nameof(IRootChild.Age)));
			Assert.NotNull(result.GetProperty(nameof(IRootChild.Value)));
			Assert.True(isAssignableFrom);
		}
	}
}

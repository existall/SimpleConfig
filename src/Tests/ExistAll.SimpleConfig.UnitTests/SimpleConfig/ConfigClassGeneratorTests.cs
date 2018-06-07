using System;
using System.Reflection;
using ExistAll.SimpleConfig.Core.Reflection;
using FastMember;
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

			var type = typeof(IRoot);

			var result = generator.GenerateType(type);

			var instance = (IRoot)Activator.CreateInstance(result);

			var isAssignableFrom = type.IsInstanceOfType(instance);

			Assert.True(isAssignableFrom);
		}

		[Fact]
		public void GenerateType_WhenGivenAnInterfaceInheritance_ShouldCreateTypeFromDerived()
		{
			var generator = new ConfigClassGenerator();

			var type = typeof(IRootChild);

			var result = generator.GenerateType(type);

			var instance = (IRootChild)Activator.CreateInstance(result);

			var isAssignableFrom = type.IsInstanceOfType(instance);

			Assert.NotNull(result.GetProperty(nameof(IRootChild.Age)));
			Assert.NotNull(result.GetProperty(nameof(IRootChild.Value)));
			Assert.True(isAssignableFrom);
		}

	    [Fact]
	    public void Marc_Demo()
	    {
	        var generator = new ConfigClassGenerator();

	        var type = typeof(IRootChild);

	        var newClassType = generator.GenerateType(type);

	        var instance = (IRootChild)Activator.CreateInstance(newClassType);

            // by creating a new accessor from the new type we get ->
            //System.NotSupportedException: 'A non-collectible assembly may not reference a collectible assembly.'
            //var accessor = TypeAccessor.Create(result);

            // creating an accessor from the child interface
	        var accessor = TypeAccessor.Create(type);

            accessor[instance, nameof(IRootChild.Age)] = 1;

            // here we get out of range exception
	        accessor[instance, nameof(IRootChild.Value)] = "sss";

	    }
    }
}

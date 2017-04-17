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

	}

	public interface ITestInterface
	{
		string Prop1 { get; set; }
		int Prop2 { get; set; }
	}
}

using ExistAll.SimpleConfig.Core;
using NSubstitute;
using Xunit;

namespace ExistAll.SimpleConfig.UnitTests.SimpleConfig
{
	public partial class ConfigTypesExtractorTests
	{
		[Fact]
		public void ExtractConfigTypes_WhenTypeHasNoIndications_ShouldNotExtractType()
		{
			var sut = new ConfigTypesExtractor();

			var assemblyCollection = Substitute.For<IAssemblyCollection>();
			assemblyCollection.GetTypes().Returns(new[] {typeof(INonIndicationInterface)});

			var results = sut.ExtractConfigTypes(assemblyCollection, new ConfigOptions());
		
			Assert.DoesNotContain(typeof(INonIndicationInterface), results);
		}

		[Fact]
		public void ExtractConfigTypes_WhenTypeHasAttributeIndications_ShouldExtractType()
		{
			var sut = new ConfigTypesExtractor();

			var assemblyCollection = Substitute.For<IAssemblyCollection>();
			assemblyCollection.GetTypes().Returns(new[] { typeof(IAttributeIndicationInterface) });

			var results = sut.ExtractConfigTypes(assemblyCollection, new ConfigOptions());

			Assert.Contains(typeof(IAttributeIndicationInterface), results);
		}

		[Fact]
		public void ExtractConfigTypes_WhenTypeHasInterfaceIndications_ShouldExtractType()
		{
			var sut = new ConfigTypesExtractor();

			var assemblyCollection = Substitute.For<IAssemblyCollection>();
			assemblyCollection.GetTypes().Returns(new[] { typeof(IIndicationInterface) });

			var results = sut.ExtractConfigTypes(assemblyCollection, new ConfigOptions());

			Assert.Contains(typeof(IIndicationInterface), results);
		}

		[Fact]
		public void ExtractConfigTypes_WhenTypeHasConfigSuffixIndications_ShouldExtractType()
		{
			var sut = new ConfigTypesExtractor();

			var assemblyCollection = Substitute.For<IAssemblyCollection>();
			assemblyCollection.GetTypes().Returns(new[] { typeof(IIndicationInterfaceConfig) });

			var results = sut.ExtractConfigTypes(assemblyCollection, new ConfigOptions());

			Assert.Contains(typeof(IIndicationInterfaceConfig), results);
		}
	}

	internal interface INonIndicationInterface
	{
		
	}

	[ConfigSection]
	internal interface IAttributeIndicationInterface
	{

	}

	internal interface IIndicationInterface : IConfigSection
	{

	}

	internal interface IIndicationInterfaceConfig
	{

	}
}

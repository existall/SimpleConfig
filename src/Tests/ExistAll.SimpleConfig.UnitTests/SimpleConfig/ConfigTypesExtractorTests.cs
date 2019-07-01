using System;
using System.Collections.Generic;
using System.Reflection;
using ExistAll.SimpleConfig.Core;
using Xunit;

namespace ExistAll.SimpleConfig.UnitTests.SimpleConfig
{
    public class ConfigTypesExtractorTests
	{
        [Fact]
		public void ExtractConfigTypes_WhenTypeHasNoIndications_ShouldNotExtractType()
		{
			var sut = new ConfigTypesExtractor();

			var assemblyCollection = MockAssemblies(typeof(INonIndicationInterface));

			var results = sut.ExtractConfigTypes(assemblyCollection, new ConfigOptions());

			Assert.DoesNotContain(typeof(INonIndicationInterface), results);
		}

		[Fact]
		public void ExtractConfigTypes_WhenTypeHasAttributeIndications_ShouldExtractType()
		{
			var sut = new ConfigTypesExtractor();

			var assemblyCollection = MockAssemblies(typeof(IAttributeIndicationInterface));

			var results = sut.ExtractConfigTypes(assemblyCollection, new ConfigOptions());

			Assert.Contains(typeof(IAttributeIndicationInterface), results);
		}

		[Fact]
		public void ExtractConfigTypes_WhenTypeHasInterfaceIndications_ShouldExtractType()
		{
			var sut = new ConfigTypesExtractor();

			var assemblyCollection = MockAssemblies(typeof(IIndicationInterface));

			var results = sut.ExtractConfigTypes(assemblyCollection, new ConfigOptions());

			Assert.Contains(typeof(IIndicationInterface), results);
		}

		[Fact]
		public void ExtractConfigTypes_WhenTypeHasConfigSuffixIndications_ShouldExtractType()
		{
			var sut = new ConfigTypesExtractor();

			var assemblyCollection = MockAssemblies(typeof(IIndicationInterfaceConfig));

			var results = sut.ExtractConfigTypes(assemblyCollection, new ConfigOptions());

			Assert.Contains(typeof(IIndicationInterfaceConfig), results);
		}

		private IEnumerable<Assembly> MockAssemblies(Type returnType)
		{
			return new[] {returnType.GetTypeInfo().Assembly};
		}
	}
}

using System;
using System.Collections.Generic;
using System.Reflection;
using ExistAll.SimpleSettings.Core;
using Xunit;

namespace ExistAll.SimpleSettings.UnitTests.SimpleSettings
{
    public class SettingsTypesExtractorTests
	{
        [Fact]
		public void ExtractSettingsTypes_WhenTypeHasNoIndications_ShouldNotExtractType()
		{
			var sut = new SettingsTypesExtractor();

			var assemblyCollection = MockAssemblies(typeof(INonIndicationInterface));

			var results = sut.ExtractSettingsTypes(assemblyCollection, new SettingsOptions());

			Assert.DoesNotContain(typeof(INonIndicationInterface), results);
		}

		[Fact]
		public void ExtractSettingsTypes_WhenTypeHasAttributeIndications_ShouldExtractType()
		{
			var sut = new SettingsTypesExtractor();

			var assemblyCollection = MockAssemblies(typeof(IAttributeIndicationInterface));

			var results = sut.ExtractSettingsTypes(assemblyCollection, new SettingsOptions());

			Assert.Contains(typeof(IAttributeIndicationInterface), results);
		}

		[Fact]
		public void ExtractSettingsTypes_WhenTypeHasInterfaceIndications_ShouldExtractType()
		{
			var sut = new SettingsTypesExtractor();

			var assemblyCollection = MockAssemblies(typeof(IIndicationInterface));

			var results = sut.ExtractSettingsTypes(assemblyCollection, new SettingsOptions());

			Assert.Contains(typeof(IIndicationInterface), results);
		}

		[Fact]
		public void ExtractSettingsTypes_WhenTypeHasSettingsSuffixIndications_ShouldExtractType()
		{
			var sut = new SettingsTypesExtractor();

			var assemblyCollection = MockAssemblies(typeof(IIndicationInterfaceSettings));

			var results = sut.ExtractSettingsTypes(assemblyCollection, new SettingsOptions());

			Assert.Contains(typeof(IIndicationInterfaceSettings), results);
		}

		private IEnumerable<Assembly> MockAssemblies(Type returnType)
		{
			return new[] {returnType.GetTypeInfo().Assembly};
		}
	}
}

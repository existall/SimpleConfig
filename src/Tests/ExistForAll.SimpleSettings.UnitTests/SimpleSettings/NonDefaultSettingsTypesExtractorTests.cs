using System;
using System.Collections.Generic;
using System.Reflection;
using ExistForAll.SimpleSettings.Core;
using Xunit;

namespace ExistForAll.SimpleSettings.UnitTests.SimpleSettings
{
    public class NonDefaultSettingsTypesExtractorTests
	{
		[Fact]
		public void ExtractSettingsTypes_WhenTypeHasNonDefaultAttributeIndication_ShouldExtractType()
		{
			var sut = new SettingsTypesExtractor();

			var assemblyCollection = MockAssemblies(typeof(INonDefaultAttributeInterface));

			var results = sut.ExtractSettingsTypes(assemblyCollection, new SettingsOptions()
			{
				AttributeType = typeof(SomeOtherAttribute)
			});

			Assert.Contains(typeof(INonDefaultAttributeInterface), results);
		}

		[Fact]
		public void ExtractSettingsTypes_WhenTypeHasNonDefaultInterfaceIndidation_ShouldExtractType()
		{
			var sut = new SettingsTypesExtractor();

			var assemblyCollection = MockAssemblies(typeof(INonDefaultInterfaceInterface));

			var results = sut.ExtractSettingsTypes(assemblyCollection, new SettingsOptions()
			{
				InterfaceBase = typeof(INonDefaultInterfaceIndication)
			});

			Assert.Contains(typeof(INonDefaultInterfaceInterface), results);
		}

		[Fact]
		public void ExtractSettingsTypes_WhenTypeHasNonDefaultSuffixIndidation_ShouldExtractType()
		{
			var sut = new SettingsTypesExtractor();

			var assemblyCollection = MockAssemblies(typeof(INonDefaultSuffixIndicationInterfaceSomeSuffix));

			var results = sut.ExtractSettingsTypes(assemblyCollection, new SettingsOptions()
			{
				SettingsSuffix = "SomeSuffix"
			});

			Assert.Contains(typeof(INonDefaultSuffixIndicationInterfaceSomeSuffix), results);
		}

		private IEnumerable<Assembly> MockAssemblies(Type returnType)
		{
			return new Assembly[] {returnType.GetTypeInfo().Assembly};
		}

		public interface INonDefaultSuffixIndicationInterfaceSomeSuffix
		{

		}

		public interface INonDefaultInterfaceIndication
		{

		}

		public interface INonDefaultInterfaceInterface : INonDefaultInterfaceIndication
		{

		}

		[SomeOther]
		public interface INonDefaultAttributeInterface
		{

		}

		public class SomeOtherAttribute : Attribute
		{

		}
	}
}

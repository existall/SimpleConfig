using System;
using System.Collections.Generic;
using System.Reflection;
using ExistAll.SimpleConfig.Core;
using Xunit;

namespace ExistAll.SimpleConfig.UnitTests.SimpleConfig
{
    public class NonDefaultConfigTypesExtractorTests
	{
		[Fact]
		public void ExtractConfigTypes_WhenTypeHasNonDefaultAttributeIndication_ShouldExtractType()
		{
			var sut = new ConfigTypesExtractor();

			var assemblyCollection = MockAssemblies(typeof(INonDefaultAttributeInterface));

			var results = sut.ExtractConfigTypes(assemblyCollection, new ConfigOptions()
			{
				AttributeType = typeof(SomeOtherAttribute)
			});

			Assert.Contains(typeof(INonDefaultAttributeInterface), results);
		}

		[Fact]
		public void ExtractConfigTypes_WhenTypeHasNonDefaultInterfaceIndidation_ShouldExtractType()
		{
			var sut = new ConfigTypesExtractor();

			var assemblyCollection = MockAssemblies(typeof(INonDefaultInterfaceInterface));

			var results = sut.ExtractConfigTypes(assemblyCollection, new ConfigOptions()
			{
				InterfaceBase = typeof(INonDefaultInterfaceIndication)
			});

			Assert.Contains(typeof(INonDefaultInterfaceInterface), results);
		}

		[Fact]
		public void ExtractConfigTypes_WhenTypeHasNonDefaultSuffixIndidation_ShouldExtractType()
		{
			var sut = new ConfigTypesExtractor();

			var assemblyCollection = MockAssemblies(typeof(INonDefaultSuffixIndicationInterfaceSomeSuffix));

			var results = sut.ExtractConfigTypes(assemblyCollection, new ConfigOptions()
			{
				ConfigSuffix = "SomeSuffix"
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

using System;
using ExistAll.SimpleConfig.Core;
using NSubstitute;
using Xunit;

namespace ExistAll.SimpleConfig.UnitTests.SimpleConfig
{
    public class NonDefaultConfigTypesExtractorTests
	{
		[Fact]
		public void ExtractConfigTypes_WhenTypeHasNonDefaultAttribueIndidation_ShouldExtractType()
		{
			var sut = new ConfigTypesExtractor();

			var assemblyCollection = Substitute.For<IAssemblyCollection>();
			assemblyCollection.GetTypes().Returns(new[] { typeof(INonDefaultAttributeInterface) });

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

			var assemblyCollection = Substitute.For<IAssemblyCollection>();
			assemblyCollection.GetTypes().Returns(new[] { typeof(INonDefaultInterfaceInterface) });

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

			var assemblyCollection = Substitute.For<IAssemblyCollection>();
			assemblyCollection.GetTypes().Returns(new[] { typeof(INonDefaultSuffixIndicationInterfaceSomeSuffix) });

			var results = sut.ExtractConfigTypes(assemblyCollection, new ConfigOptions()
			{
				ConfigSuffix = "SomeSuffix"
			});

			Assert.Contains(typeof(INonDefaultSuffixIndicationInterfaceSomeSuffix), results);
		}

		internal interface INonDefaultSuffixIndicationInterfaceSomeSuffix
		{
			
		}

		internal interface INonDefaultInterfaceIndication
		{

		}

		internal interface INonDefaultInterfaceInterface : INonDefaultInterfaceIndication
		{

		}

		[SomeOther]
		internal interface INonDefaultAttributeInterface
		{

		}

		internal class SomeOtherAttribute : Attribute
		{

		}
	}
}

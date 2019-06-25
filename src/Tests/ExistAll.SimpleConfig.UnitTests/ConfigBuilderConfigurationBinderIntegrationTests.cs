using System;
using System.IO;
using ExistAll.SimpleConfig.Binder;
using ExistAll.SimpleConfig.Binders;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ExistAll.SimpleConfig.UnitTests
{
	public class ConfigBuilderConfigurationBinderIntegrationTests
	{
		[Fact]
		public void Build_WhenInterfaceHasConfigNameSet_ShouldGetDataFromName()
		{
			var configuration = GetConfiguration();

			var sut = BuildSutWithBinder(new ConfigurationBinder(configuration))
				.AddAssembly(GetType().Assembly);
			
			var configCollection = sut.Build();

			var config = configCollection.GetConfig<IConfigInterfaceRootName>();

			Assert.Equal(44, config.Value);
		}
		
		[Fact]
		public void Build_WhenInterfaceHasRootNameAndPropertyName_ShouldGetTheCorrectData()
		{
			var configuration = GetConfiguration();

			var sut = BuildSutWithBinder(new ConfigurationBinder(configuration))
				.AddAssembly(GetType().Assembly);

			var configCollection = sut.Build();

			var config = configCollection.GetConfig<ISectionNameAndProperty>();

			Assert.Equal("some-value", config.Value);
		}
		
		[Fact]
		public void Build_WhenDataIsInAppSettingsRootWithMatchingNames_ShouldGetValue()
		{
			var configuration = GetConfiguration();

			var sut = BuildSutWithBinder(new ConfigurationBinder(configuration))
				.AddAssembly(GetType().Assembly);

			var result = sut.Build();

			var config = result.GetConfig<IRoot>();

			Assert.Equal("albert", config.Value);
		}

		[Fact]
		public void Build_WhenConfigurationBinderHasDifferentRootName_ShouldGetDataFromInnerRootSections()
		{
			var configuration = GetConfiguration();
				
			var sut = BuildSutWithBinder(new ConfigurationBinder(configuration, "appSettings"))
				.AddAssembly(GetType().Assembly);

			var result = sut.Build();

			var config = result.GetConfig<IRoot>();

			Assert.Equal("value" ,config.Value);
		}

		[Fact]
		public void Build_WhenMemoryBinderSetValues_ShouldSetProperData()
		{
			var value = Guid.NewGuid().ToString();
			var collection = new InMemoryCollection();
			collection.Add("Root", "Value", value);

			var sut = BuildSutWithBinder(new InMemoryBinder(collection))
				.AddAssembly(GetType().Assembly);

			var result = sut.Build();

			var config = result.GetConfig<IRoot>();

			Assert.Equal(value, config.Value);
		}

		private IConfiguration GetConfiguration()
		{
			return new ConfigurationBuilder()
				.AddJsonFile(Path.Combine(AppContext.BaseDirectory, "../../../appSettings.json"))
				.Build();
		}

		private ConfigBuilder BuildSutWithBinder(params ISectionBinder[] binders)
		{
			var sut = ConfigBuilder.CreateBuilder();
			
			foreach (var sectionBinder in binders)
			{
				sut.AddSectionBinder(sectionBinder);
			}

			return sut;
		}
	}
}

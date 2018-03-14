using System;
using System.Reflection;
using ExistAll.SimpleConfig.Binder;
using Xunit;

namespace ExistAll.SimpleConfig.UnitTests.SimpleConfig
{
	public class EnvironmentVariableAttributeTests
	{	
		[Fact]
		public void Build_WhereVariableHasValue_ShouldSetProperty()
		{
			Environment.SetEnvironmentVariable("TestVar","value");
			
			var sut = new ConfigBuilder();
			
			var configCollection = sut.Build(new[] { GetType().GetTypeInfo().Assembly });
			var config = configCollection.GetConfig<IWithEnvironmentVariable>();

			Assert.Null(config.Path);
			
			Environment.SetEnvironmentVariable("TestVar", null);
		}
		
		[Fact]
		public void TryGetValue_WhenVariablePresentAndHasOverride_ShouldSetValueFromBinder()
		{
			const string value = "value";

			var collection = new InMemoryCollection();

			collection.Add("WithEnvironmentVariable","Path", value);
			var sut = new ConfigBuilder();
			sut.AddSectionBinder(new InMemoryBinder(collection));

			var configCollection = sut.Build(new[] { GetType().GetTypeInfo().Assembly });
			var config = configCollection.GetConfig<IWithEnvironmentVariable>();

			Assert.Equal(config.Path, value);
		}
	}
}
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
			var sut = new ConfigBuilder();
			
			var configCollection = sut.Build(new[] { GetType().GetTypeInfo().Assembly }, new ConfigOptions());
			var config = configCollection.GetConfig<IWithEnvironmentVariable>();
			
			Assert.NotNull(config.Path);
		}
		
		[Fact]
		public void TryGetValue_WhenVariablePresentAndHasOverride_ShouldSetValueFromBinder()
		{
			const string value = "value";

			var collection = new InMemoryCollection();

			collection.Add("WithEnvironmentVariable","Path", value);
			var sut = new ConfigBuilder();
			sut.Add(new InMemoryBinder(collection));

			var configCollection = sut.Build(new[] { GetType().GetTypeInfo().Assembly }, new ConfigOptions());
			var config = configCollection.GetConfig<IWithEnvironmentVariable>();

			Assert.Equal(config.Path, value);
		}
	}
}
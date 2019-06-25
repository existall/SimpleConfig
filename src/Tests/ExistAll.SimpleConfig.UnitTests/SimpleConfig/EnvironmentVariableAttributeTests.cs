using System;
using System.Reflection;
using ExistAll.SimpleConfig.Binder;
using Xunit;

namespace ExistAll.SimpleConfig.UnitTests.SimpleConfig
{
	public class EnvironmentVariableAttributeTests
	{
		public const string EnvironmentVariable = "env-var";

		[Fact]
		public void Build_WhereVariableHasValue_ShouldSetProperty()
		{
			var guid = Guid.NewGuid().ToString();

			using (new DisposableEnvironmentVariable(EnvironmentVariable, guid))
			{
				var sut = ConfigBuilder.CreateBuilder().AddAssembly(GetType().Assembly);

				var result = sut.Build();

				var config = result.GetConfig<IWithEnvironmentVariable>();

				Assert.Equal(guid, config.EnvironmentVariable);
			}
		}
		
		[Fact]
		public void TryGetValue_WhenVariablePresentAndHasOverride_ShouldSetValueFromBinder()
		{
			var guid = Guid.NewGuid().ToString();

			using (new DisposableEnvironmentVariable(EnvironmentVariable, guid))
			{
				var collection = new InMemoryCollection();
				collection.Add(nameof(IWithEnvironmentVariable).TrimStart('I'), nameof(IWithEnvironmentVariable.EnvironmentVariable), guid);
				var sut = ConfigBuilder.CreateBuilder().AddAssembly(GetType().GetTypeInfo().Assembly);
				sut.AddSectionBinder(new InMemoryBinder(collection));
				var configCollection = sut.Build();
				var config = configCollection.GetConfig<IWithEnvironmentVariable>();

				Assert.Equal(guid, config.EnvironmentVariable);
			}
		}
	}
}
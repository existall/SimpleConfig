using System;
using System.Reflection;
using ExistAll.SimpleConfig.Binder;
using ExistAll.SimpleConfig.Binders;
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
                var sut = ConfigBuilder.CreateBuilder(x => x.AddEnvironmentVariable());

                var result = sut.ScanAssemblies(GetType().Assembly);

				var config = result.GetConfig<IWithEnvironmentVariable>();

				Assert.Equal(guid, config.EnvironmentVariable);
			}
		}
		
		[Fact]
		public void TryGetValue_WhenEnvVariableIsLast_ShouldSetValueFromEnv()
		{
			var guid = Guid.NewGuid().ToString();
			var badGuid = Guid.NewGuid().ToString();
			
            using (new DisposableEnvironmentVariable(EnvironmentVariable, guid))
            {
                var collection = new InMemoryCollection();
                collection.Add(nameof(IWithEnvironmentVariable).TrimStart('I'),
	                EnvironmentVariable, badGuid);

                var sut = ConfigBuilder.CreateBuilder(x => 
                { 
	                x.AddInMemoryCollection(collection)
	                .AddEnvironmentVariable(); 
                });

                var config = sut.GetConfig<IWithEnvironmentVariable>();

                Assert.Equal(guid, config.EnvironmentVariable);
            }
        }
		
		[Fact]
		public void TryGetValue_WhenMemoryCollectionIsLast_ShouldSetValueFromMemoryBinder()
		{
			var guid = Guid.NewGuid().ToString();
			var badGuid = Guid.NewGuid().ToString();
			
			using (new DisposableEnvironmentVariable(EnvironmentVariable, badGuid))
			{
				var collection = new InMemoryCollection();
				collection.Add(nameof(IWithEnvironmentVariable).TrimStart('I'),
					EnvironmentVariable, guid);

				var sut = ConfigBuilder.CreateBuilder(x => 
				{ 
					x.AddEnvironmentVariable()
						.AddInMemoryCollection(collection); 
				});

				var config = sut.GetConfig<IWithEnvironmentVariable>();

				Assert.Equal(guid, config.EnvironmentVariable);
			}
		}
	}
}
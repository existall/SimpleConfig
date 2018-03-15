using System;
using System.IO;
using System.Reflection;
using ExistAll.SimpleConfig.Binder;
using ExistAll.SimpleConfig.Binders;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ExistAll.SimpleConfig.UnitTests
{
	public class ConfigBuilderWithConfigurationTests
	{

		[Fact]
		public void Test3()
		{
			var configuration = new ConfigurationBuilder()
				.AddJsonFile(Path.Combine(AppContext.BaseDirectory, "../../../appSettings.json")).Build();

			var t = new ConfigBuilder();
			t.Add(new ConfigurationBinder(configuration));

			var configCollection = t.Build(new[] { GetType().GetTypeInfo().Assembly }, new ConfigOptions());

			var config = configCollection.GetConfig<IInterfaceThree>();

			Assert.Equal(34, config.Age);
		}
		
		[Fact]
		public void Test4()
		{
			var configuration = new ConfigurationBuilder()
				.AddJsonFile(Path.Combine(AppContext.BaseDirectory, "../../../appSettings.json")).Build();

			var t = new ConfigBuilder();
			t.Add(new ConfigurationBinder(configuration));

			var configCollection = t.Build(new[] { GetType().GetTypeInfo().Assembly }, new ConfigOptions());

			var config = configCollection.GetConfig<IInterfaceFour>();

			Assert.Equal(55, config.Age);
		}
		
		[Fact]
		public void Test()
		{
			var configuration = new ConfigurationBuilder()
				.AddJsonFile(Path.Combine(AppContext.BaseDirectory, "../../../appSettings.json")).Build();

			var t = new ConfigBuilder();
			t.AddSectionBinder(new ConfigurationBinder(configuration));


			var configCollection = t.Build(new []{ GetType().GetTypeInfo().Assembly });
			var config = configCollection.GetConfig<IInterfaceOne>();
		    var configx = configCollection.GetConfig<IInterfaceTwo>();
        }

		[Fact]
		public void Test2()
		{
			var configuration = new ConfigurationBuilder()
				.AddJsonFile(Path.Combine(AppContext.BaseDirectory, "../../../appSettings.json")).Build();

			var t = new ConfigBuilder();
			t.Add(new ConfigurationBinder(configuration,"appSettings"));


			var configCollection = t.Build(new[] { GetType().GetTypeInfo().Assembly }, new ConfigOptions());
			var config = configCollection.GetConfig<IInterfaceOne>();
			var configx = configCollection.GetConfig<IInterfaceTwo>();
		}

		[Fact]
		public void Test1()
		{
			var co = new InMemoryCollection();
			co.Add("X1", "Name", "goni");

			var t = new ConfigBuilder();
			t.AddSectionBinder(new InMemoryBinder(co));


			var configCollection = t.Build(new []{GetType().GetTypeInfo().Assembly});
			var config = configCollection.GetConfig<IInterfaceOne>();
			var config1 = configCollection.GetConfig<IInterfaceTwo>();
		}
	}
}

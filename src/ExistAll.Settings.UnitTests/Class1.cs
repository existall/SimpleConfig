using System;
using System.IO;
using System.Reflection;
using ExistAll.Settings.Binder;
using ExistAll.Settings.Binders;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ExistAll.Settings.UnitTests
{
	public class Class1
	{
		[Fact]
		public void Test()
		{
			var configuration = new ConfigurationBuilder().AddJsonFile(Path.Combine(AppContext.BaseDirectory, "../../../appSettings.json")).Build();

			var collection = new AssemblyCollection()
				.AddFullAssemblyHolder(this.GetType().GetTypeInfo().Assembly);

			var t = new SettingsBuilder();
			t.Add(new ConfigurationBinder(configuration));


			var settingsCollection = t.Build(collection, new SettingsOptions());
			var settings = settingsCollection.GetSettings<IX1>();

		}

		[Fact]
		public void Test1()
		{
			var collection = new AssemblyCollection()
				.AddFullAssemblyHolder(this.GetType().GetTypeInfo().Assembly);

			var co = new InMemoryCollection();
			co.Add("X1", "Name", "goni");

			var t = new SettingsBuilder();
			t.Add(new InMemoryBinder(co));


			var settingsCollection = t.Build(collection, new SettingsOptions());
			var settings = settingsCollection.GetSettings<IX1>();
			var settings1 = settingsCollection.GetSettings<IX2>();

		}
	}
}

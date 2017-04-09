using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using ExistAll.Settings.Binders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Xunit;
using	Microsoft.Extensions.Configuration.Json;

namespace ExistAll.Settings.UnitTests
{
    public class Class1
    {
		[Fact]
	    public void Test()
		{
			var configuration = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();

			var collection = new AssemblyCollection()
				.AddFullAssemblyHolder(this.GetType().GetTypeInfo().Assembly);

			var t = new SettingsBuilder();
			t.Add(new ConfigurationBinder(configuration));


			var settingsCollection = t.Build(collection, new SettingsOptions());
			var settings = settingsCollection.GetSettings<IX1>();

		}
    }
}

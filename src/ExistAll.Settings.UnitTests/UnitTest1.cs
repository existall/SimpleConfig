using System;
using System.Reflection;
using ExistAll.Settings.Core.Reflection;
using Xunit;

namespace ExistAll.Settings.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
			SettingsClassGenerator generator = new SettingsClassGenerator();
	        var generateType = generator.GenerateType(typeof(IX1));
			var generateType1 = generator.GenerateType(typeof(IX2));

	        var instance = (IX1)Activator.CreateInstance(generateType);
        }

		[Fact]
		public void Test2()
		{
			var collection = new AssemblyCollection()
				.AddFullAssemblyHolder(this.GetType().GetTypeInfo().Assembly);

			var t = new SettingsBuilder().Build(collection, new SettingsOptions());

			var settings = t.GetSettings<IX1>();
		}
	}

	
	public interface IX1 : ISettingSection
	{
	
		string Name { get; set; }
	}

	[SettingsSectionAttribue]
	public interface IX2
	{
		[DefaultValue(4)]
		int Number { get; set; }
	}
}

using System;
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
	        //var generateType = generator.GenerateType(typeof(X1));
			var generateType1 = generator.GenerateType(typeof(X2));
		}
    }

	public interface X1
	{
		string Name { get; set; }
	}

	public interface X2
	{
		int Number { get; set; }
	}
}

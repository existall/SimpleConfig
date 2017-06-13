using System;
using System.Reflection;
using ExistAll.SimpleConfig.Core.Reflection;
using Xunit;

namespace ExistAll.SimpleConfig.UnitTests
{
	public class UnitTest1
	{
		[Fact]
		public void Test1()
		{
			ConfigClassGenerator generator = new ConfigClassGenerator();
			var generateType = generator.GenerateType(typeof(IX1));
			var generateType1 = generator.GenerateType(typeof(IX2));

			var instance = (IX1)Activator.CreateInstance(generateType);
		}

		[Fact]
		public void Test2()
		{

			var configCollection = new ConfigBuilder().Build(new []{ GetType().GetTypeInfo().Assembly }, new ConfigOptions());

			var config = configCollection.GetConfig<IX1>();

			foreach (var configItem in configCollection)
			{
				Type interfaceType = configItem.Key;
				object configImplemintation = configItem.Value;
			}
		}
	}


	public interface IX1 : IConfigSection
	{
		string Name { get; set; }
	}

	[ConfigSection]
	public interface IX2
	{
		[DefaultValue(4)]
		int Number { get; set; }

        [DefaultValue("x","y")]
        string[] Array { get; set; }

        [DefaultValue(1, 2)]
        int[] Array2 { get; set; }
	}

	[ConfigSection]
	public interface IEmailSenderConfig
	{
		[DefaultValue("SomeUrl")]
		string EmailServiceUrl { get; set; }

		[DefaultValue(3)]
		int Retries { get; set; }
	}
}

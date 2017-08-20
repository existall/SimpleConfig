using System.Reflection;
using ExistAll.SimpleConfig.Binder;
using Xunit;

namespace ExistAll.SimpleConfig.UnitTests.Core.AspNet
{
	public class ConfigBuilderAspNetEnvironmentsTests
	{
		[Theory]
		[InlineData(Environments.Development)]
		[InlineData(Environments.Staging)]
		[InlineData(Environments.Production)]
		public void Build_WhenEnvironmetVariableMatch_ShouldReturnTHeCorrectValue(string environmet)
		{
			AspNetTestHelper.SetEnvVariable(environmet);

			var sut = new ConfigBuilder();

			var configCollection = sut.Build(new[] {GetType().GetTypeInfo().Assembly}, new ConfigOptions());
			var config = configCollection.GetConfig<IEnvSettings>();

			var result = config.Value;
			
			AspNetTestHelper.ResetEnvVariable();
			
			Assert.StrictEqual(environmet,result);
			
		}
	}
}
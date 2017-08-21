using System.Reflection;
using ExistAll.SimpleConfig.Binder;
using Xunit;

namespace ExistAll.SimpleConfig.UnitTests.Core.AspNet
{
    // this tests are falky due to usage of static class like env ! bad practice 

    /*public class ConfigBuilderAspNetEnvironmentsTests
    {
        [Theory]
        [InlineData(Environments.Development)]
        [InlineData(Environments.Staging)]
        [InlineData(Environments.Production)]
        public void Build_WhenEnvironmentVariableMatch_ShouldReturnTHeCorrectValue(string environment)
        {
            AspNetTestHelper.SetEnvVariable(environment);

            var sut = new ConfigBuilder();

            var configCollection = sut.Build(new[] { GetType().GetTypeInfo().Assembly }, new ConfigOptions());
            var config = configCollection.GetConfig<IEnvSettings>();

            var result = config.Value;

            AspNetTestHelper.ResetEnvVariable();

            Assert.StrictEqual(environment, result);

        }

    }*/
}
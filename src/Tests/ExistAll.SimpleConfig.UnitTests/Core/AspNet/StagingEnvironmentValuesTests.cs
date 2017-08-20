using System.Net;
using ExistAll.SimpleConfig.Core.AspNet;
using Xunit;

namespace ExistAll.SimpleConfig.UnitTests.Core.AspNet
{
	public class StagingEnvironmentValuesTests
	{
		[Fact]
		public void ShouldUse_WhenEnvironmentIsDev_ShouldReturnTrue()
		{
			AspNetTestHelper.SetEnvVariable(Environments.Staging);

			var sut = new StagingDefaultValue("hello");

			Assert.True(sut.ShouldUse);

			AspNetTestHelper.ResetEnvVariable();
		}

		[Fact]
		public void ShouldUse_WhenEnvironmentIsNotDev_ShouldReturnFalse()
		{
			var sut = new StagingDefaultValue("hello");

			Assert.False(sut.ShouldUse);

			AspNetTestHelper.ResetEnvVariable();
		}

		[Fact]
		public void ShouldUse_WhenEnvironmentIsStaging_ShouldReturnFalse()
		{
			AspNetTestHelper.SetEnvVariable(Environments.Production);

			var sut = new StagingDefaultValue("hello");

			Assert.False(sut.ShouldUse);

			AspNetTestHelper.ResetEnvVariable();
		}
	}
}
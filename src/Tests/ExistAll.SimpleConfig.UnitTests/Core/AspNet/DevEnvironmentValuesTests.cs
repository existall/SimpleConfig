using ExistAll.SimpleConfig.Core.AspNet;
using Xunit;

namespace ExistAll.SimpleConfig.UnitTests.Core.AspNet
{
	public class DevEnvironmentValuesTests
	{
		private const string VariableName = "ASPNETCORE_ENVIRONMENT";

		[Fact]
		public void ShouldUse_WhenEnvironmentIsDev_ShouldReturnTrue()
		{
			using (new DisposableEnvironmentVariable(VariableName, Environments.Development))
			{
				var sut = new DevelopmentDefaultValue("hello");

				Assert.True(sut.ShouldUse);
			}
		}

		[Fact]
		public void ShouldUse_WhenEnvironmentIsNotDev_ShouldReturnFalse()
		{
			var sut = new DevelopmentDefaultValue("hello");

			Assert.False(sut.ShouldUse);
		}

		[Fact]
		public void ShouldUse_WhenEnvironmentIsStaging_ShouldReturnTrue()
		{
			using (new DisposableEnvironmentVariable(VariableName, Environments.Staging))
			{
				var sut = new StagingDefaultValue("hello");

				Assert.True(sut.ShouldUse);
			}
		}
	}
}
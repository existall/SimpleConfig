using ExistAll.SimpleConfig.Core.AspNet;
using Xunit;

namespace ExistAll.SimpleConfig.UnitTests.Core.AspNet
{
	public class StagingEnvironmentValuesTests
	{
		[Fact]
		public void ShouldUse_WhenEnvironmentIsDev_ShouldReturnTrue()
		{
			using (new AspCoreDisposableEnvironmentVariable(Environments.Staging))
			{
				var sut = new StagingDefaultValue("hello");

				Assert.True(sut.ShouldUse);
			}
		}

		[Fact]
		public void ShouldUse_WhenEnvironmentIsNotDev_ShouldReturnFalse()
		{
			var sut = new StagingDefaultValue("hello");

			Assert.False(sut.ShouldUse);
		}

		[Fact]
		public void ShouldUse_WhenEnvironmentIsStaging_ShouldReturnFalse()
		{
			using (new AspCoreDisposableEnvironmentVariable(Environments.Production))
			{
				var sut = new StagingDefaultValue("hello");

				Assert.False(sut.ShouldUse);
			}
		}
	}
}
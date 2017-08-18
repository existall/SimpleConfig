using System;
using ExistAll.SimpleConfig.Core.AspNet;
using Xunit;

namespace ExistAll.SimpleConfig.UnitTests.Core.AspNet
{
	public class DevEnvironmentValuesTests
	{
		private const string VariableName = "ASPNETCORE_ENVIRONMENT";

		public void ResetEnvironmentVariable()
		{
			Environment.SetEnvironmentVariable(VariableName, null);
		}


		[Fact]
		public void ShouldUse_WhenEnvironmentIsDev_ShouldReturnTrue()
		{
			Environment.SetEnvironmentVariable(VariableName, Environments.Development);

			var sut = new DevelopmentDefaultValue("hello");

			Assert.True(sut.ShouldUse);

			ResetEnvironmentVariable();
		}

		[Fact]
		public void ShouldUse_WhenEnvironmentIsNotDev_ShouldReturnFalse()
		{
			var sut = new DevelopmentDefaultValue("hello");

			Assert.False(sut.ShouldUse);

			ResetEnvironmentVariable();
		}

		[Fact]
		public void ShouldUse_WhenEnvironmentIsStaging_ShouldReturnFalse()
		{
			Environment.SetEnvironmentVariable(VariableName, Environments.Staging);

			var sut = new DevelopmentDefaultValue("hello");

			Assert.False(sut.ShouldUse);

			ResetEnvironmentVariable();
		}
	}
}
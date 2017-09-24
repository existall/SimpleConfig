﻿using System.Reflection;
using Xunit;

namespace ExistAll.SimpleConfig.Tests.Frameworks
{
	public class AppSettingsAttributeTests
	{
		[Fact]
		public void Build_WhereVariableHasValue_ShouldSetProperty()
		{
			var sut = new ConfigBuilder();

			var configCollection = sut.Build(new[] { GetType().GetTypeInfo().Assembly }, new ConfigOptions());
			var config = configCollection.GetConfig<IWithConfigurationValue>();

			Assert.Equal(config.WithValue, TestsConstanst.AppSettingsValue);
			//Assert.Null(config.WithoutValue);
		}
	}
}
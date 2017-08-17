using System;
using Microsoft.Win32.SafeHandles;

namespace ExistAll.SimpleConfig.Core.AspNet
{
	internal static class Environments
	{
		public const string Development = "Development";
		public const string Staging = "Staging";
		public const string Production = "Production";
	}

	public class EnvironmentValueAttribute : ConditionalDefaultValueBaseAttribute
	{
		private readonly string _environment;

		public EnvironmentValueAttribute(object defaultValue, string environment)
		{
			_environment = environment;
			DefaultValue = defaultValue;
		}

		public sealed override object DefaultValue { get; set; }

		public override bool ShouldUse => _environment.ToLowerInvariant()
			.Equals(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
			?.ToLowerInvariant());
	}
}

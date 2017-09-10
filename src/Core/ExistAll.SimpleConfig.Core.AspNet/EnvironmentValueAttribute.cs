using System;

namespace ExistAll.SimpleConfig.Core.AspNet
{
	public class EnvironmentValueAttribute : ConditionalDefaultValueBaseAttribute
	{
		private readonly string _environment;

		public EnvironmentValueAttribute(object defaultValue, string environment)
		{
			_environment = environment;
			DefaultValue = defaultValue;
		}

		public sealed override object DefaultValue { get; }

		public override bool ShouldUse => _environment.ToLowerInvariant()
			.Equals(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
			?.ToLowerInvariant());
	}
}

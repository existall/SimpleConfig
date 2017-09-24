using System;

namespace ExistAll.SimpleConfig.Core.AspNet
{
	public class EnvironmentValueAttribute : DefaultValueBaseAttribute
	{
		private readonly string _environment;

		public EnvironmentValueAttribute(string environment, object defaultValue, params object[] defaultValues)
			: base(defaultValue, defaultValues)
		{
			_environment = environment;
		}

		public override bool ShouldUse => _environment.ToLowerInvariant()
			.Equals(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
			?.ToLowerInvariant());
	}
}

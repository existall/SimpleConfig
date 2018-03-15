using Microsoft.Extensions.Configuration;

namespace ExistAll.SimpleConfig.Binders
{
	public class ConfigurationBinder : ISectionBinder
	{
		public string RootSection { get; }

		private readonly IConfiguration _configuration;

		public ConfigurationBinder(IConfiguration configuration, string rootSection = null)
		{
			RootSection = rootSection;
			_configuration = configuration;
		}

		public bool TryGetValue(ConfigBindingContext bindingContext, out string value)
		{
			value = null;
			var configurationSection = _configuration.GetSection(GetSection(bindingContext));

			if (configurationSection == null)
				return false;

			value = configurationSection[bindingContext.Key];
			return value != null;
		}

		private string GetSection(ConfigBindingContext context)
		{
			if (string.IsNullOrWhiteSpace(RootSection))
				return context.Section;

			return $"{RootSection}:{context.Section}";
		}
	}
}

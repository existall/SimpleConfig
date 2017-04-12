using Microsoft.Extensions.Configuration;

namespace ExistAll.SimpleConfig.Binders
{
	public class ConfigurationBinder : ISectionBinder
	{
		private readonly IConfiguration _configuration;

		public ConfigurationBinder(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string GetValue(ConfigBindingContext bindingContext)
		{
			var configurationSection = _configuration.GetSection(bindingContext.Section);

			return configurationSection?[bindingContext.Key];
		}

		public bool TryGetValue(ConfigBindingContext bindingContext, out string value)
		{
			value = null;
			var configurationSection = _configuration.GetSection(bindingContext.Section);

			if (configurationSection == null)
				return false;

			value = configurationSection[bindingContext.Key];
			return value != null;
		}
	}
}

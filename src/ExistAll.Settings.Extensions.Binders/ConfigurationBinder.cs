using System;
using Microsoft.Extensions.Configuration;

namespace ExistAll.Settings.Extensions.Binders
{
	public class ConfigurationBinder : ISectionBinder
	{
		private readonly IConfiguration _configuration;

		public ConfigurationBinder(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string GetValue(SettingsBindingContext bindingContext)
		{
			try
			{
				var configurationSection = _configuration.GetSection(bindingContext.Section);

				return configurationSection?[bindingContext.Key];
			}
			catch (Exception e)
			{
				
			}

			return null;
		}
	}
}

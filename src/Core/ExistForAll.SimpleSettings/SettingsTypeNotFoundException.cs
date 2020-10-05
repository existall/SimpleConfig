using System;

namespace ExistForAll.SimpleSettings
{
	public class SettingsTypeNotFoundException : Exception
	{
		public SettingsTypeNotFoundException(Type settingsType)
			:base(Resources.GetSettingsNotFoundMessageFormatMessage(settingsType))
		{
		}
	}
}
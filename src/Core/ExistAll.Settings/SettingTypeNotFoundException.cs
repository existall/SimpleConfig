using System;

namespace ExistAll.Settings
{
	public class SettingTypeNotFoundException : Exception
	{
		public SettingTypeNotFoundException(Type settingsType) 
			:base(Resources.GetSettingsNotFoundMessageFormatMessage(settingsType))
		{
		}
	}
}
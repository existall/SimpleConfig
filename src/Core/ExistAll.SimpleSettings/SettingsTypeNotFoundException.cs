using System;

namespace ExistAll.SimpleSettings
{
	public class SettingsTypeNotFoundException : Exception
	{
		public SettingsTypeNotFoundException(Type settingsType)
			:base(Resources.GetSettingsNotFoundMessageFormatMessage(settingsType))
		{
		}
	}
}
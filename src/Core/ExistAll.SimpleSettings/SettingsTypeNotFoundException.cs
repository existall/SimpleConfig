using System;

namespace ExistAll.SimpleSettings
{
	public class SettingsTypeNotFoundException : Exception
	{
		public SettingsTypeNotFoundException(Type configType)
			:base(Resources.GetSettingsNotFoundMessageFormatMessage(configType))
		{
		}
	}
}
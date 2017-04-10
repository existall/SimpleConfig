using System;

namespace ExistAll.Settings
{
	public class SettingsHolder : ISettingsHolder
	{
		public SettingsHolder(Type settingsType, object settingsImplemintation)
		{
			SettingsType = settingsType;
			SettingsImplemintation = settingsImplemintation;
		}

		public Type SettingsType { get; }
		public object SettingsImplemintation { get; }
	}
}

using System;

namespace ExistAll.Settings
{
	internal interface ISettingsHolder
	{
		Type SettingsType { get; }
		object SettingsImplemintation { get; }
	}
}
using System;

namespace ExistAll.SimpleSettings
{
	internal interface ISettingsHolder
	{
		Type SettingsType { get; }
		object SettingsImplementation { get; }
	}
}
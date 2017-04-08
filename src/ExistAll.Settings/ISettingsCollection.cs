using System;
using System.Collections.Generic;

namespace ExistAll.Settings
{
	public interface ISettingsCollection
	{
	}

	public class SettingsCollection : ISettingsCollection
	{
		private readonly Dictionary<Type, ISettingsHolder> _settingsHolders = new Dictionary<Type, ISettingsHolder>();

		internal void Add(Type settingType, object impl)
		{
			_settingsHolders.Add(settingType, new SettingsHolder(settingType, impl));
		}
	}
}
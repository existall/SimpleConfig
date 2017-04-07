using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistAll.Settings
{
	public class SettingsCollection : ISettingsCollection
	{
		private readonly Dictionary<Type, ISettingsHolder> _settingsHolders = new Dictionary<Type, ISettingsHolder>();

		internal void Add(Type settingType, object impl)
		{
			_settingsHolders.Add(settingType, new SettingsHolder(settingType, impl));
		}


	}

	internal interface ISettingsHolder
	{
		Type SettingsType { get; }
		object SettingsImplemintation { get; }
	}

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

	public interface ISettingsCollection
	{
	}
}

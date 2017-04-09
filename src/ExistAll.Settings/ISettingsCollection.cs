using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ExistAll.Settings
{
	public interface ISettingsCollection : IEnumerable<KeyValuePair<Type, object>>
	{
		T GetSettings<T>(bool throwIfNotExist = false) where T : class;
	}

	public class SettingsCollection : ISettingsCollection
	{
		private readonly Dictionary<Type, ISettingsHolder> _settingsHolders = new Dictionary<Type, ISettingsHolder>();

		internal void Add(Type settingType, object impl)
		{
			_settingsHolders.Add(settingType, new SettingsHolder(settingType, impl));
		}

		public T GetSettings<T>(bool throwIfNotExist = false) where T : class
		{
			var type = typeof(T);
			ISettingsHolder holder;
			if (_settingsHolders.TryGetValue(type, out holder))
				return (T)holder.SettingsImplemintation;

			if (throwIfNotExist)
				throw new SettingTypeNotFoundException(type);

			return (T)null;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerator<KeyValuePair<Type, object>> GetEnumerator()
		{
			var dictionary = _settingsHolders.ToDictionary(x => x.Key, y => (object) y.Value);

			return dictionary.GetEnumerator();
		}
	}
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ExistAll.SimpleConfig
{
	public class ConfigCollection : IConfigCollection
	{
		private readonly Dictionary<Type, IConfigHolder> _configHolders = new Dictionary<Type, IConfigHolder>();

		internal void Add(Type configType, object impl)
		{
			_configHolders.Add(configType, new ConfigHolder(configType, impl));
		}

		public T GetConfig<T>(bool throwIfNotExist = false) where T : class
		{
			var type = typeof(T);
			IConfigHolder holder;
			if (_configHolders.TryGetValue(type, out holder))
				return (T)holder.ConfigImplementation;

			if (throwIfNotExist)
				throw new ConfigTypeNotFoundException(type);

			return (T)null;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerator<KeyValuePair<Type, object>> GetEnumerator()
		{
			var dictionary = _configHolders.ToDictionary(x => x.Key, y => y.Value.ConfigImplementation);

			return dictionary.GetEnumerator();
		}
	}
}
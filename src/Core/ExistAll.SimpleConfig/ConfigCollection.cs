using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExistAll.SimpleConfig
{
	internal class ConfigCollection : IConfigCollection
	{
		private readonly Dictionary<Type, IConfigHolder> _configHolders = new Dictionary<Type, IConfigHolder>();

        internal void Add(Type configType, object impl)
		{
			_configHolders.Add(configType, new ConfigHolder(configType, impl));
		}

		public object GetConfig(Type type)
		{
			if (!type.GetTypeInfo().IsInterface)
			{
				throw new InvalidOperationException(Resources.TypeIsNotInterface(type.Name));
			}
			
			return _configHolders.TryGetValue(type, out var holder) ? holder.ConfigImplementation : throw new ConfigTypeNotFoundException(type);
		}

        public bool TryGetConfig(Type type, out object config)
        {
            if (!type.GetTypeInfo().IsInterface)
            {
                throw new InvalidOperationException(Resources.TypeIsNotInterface(type.Name));
            }

            if(_configHolders.TryGetValue(type, out var holder))
            {
                config = holder.ConfigImplementation;
                return true;
            }

            config = null;
            return false;
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
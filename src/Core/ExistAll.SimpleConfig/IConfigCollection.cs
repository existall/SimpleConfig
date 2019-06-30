using System;
using System.Collections.Generic;

namespace ExistAll.SimpleConfig
{
	public interface IConfigCollection : IEnumerable<KeyValuePair<Type, object>>
	{
		object GetConfig(Type type);
        bool TryGetConfig(Type type, out object config);
    }
}
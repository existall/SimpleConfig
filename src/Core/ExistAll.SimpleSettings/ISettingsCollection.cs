using System;
using System.Collections.Generic;

namespace ExistAll.SimpleSettings
{
	public interface ISettingsCollection : IEnumerable<KeyValuePair<Type, object>>
	{
		object GetSettings(Type type);
        bool TryGetSettings(Type type, out object settings);
    }
}
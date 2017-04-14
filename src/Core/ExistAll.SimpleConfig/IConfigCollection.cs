using System;
using System.Collections.Generic;

namespace ExistAll.SimpleConfig
{
	public interface IConfigCollection : IEnumerable<KeyValuePair<Type, object>>
	{
		T GetConfig<T>(bool throwIfNotExist = false) where T : class;
	}
}
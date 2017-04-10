using System;
using System.Collections.Generic;

namespace ExistAll.Settings.Binder
{
	public class InMemoryCollection
	{
		private readonly Dictionary<string, string> _inner = new Dictionary<string, string>();

		public void Add(string section, string key, string value)
		{
			if (section == null) throw new ArgumentNullException(nameof(section));
			if (key == null) throw new ArgumentNullException(nameof(key));

			_inner.Add(CreateKey(section, key), value);
		}

		internal string GetValue(string section, string key)
		{
			string value = null;
			_inner.TryGetValue(CreateKey(section, key),out value);
			return value;
		}

		private static string CreateKey(string section, string key)
		{
			return section + ":" + key;
		}
	}
}
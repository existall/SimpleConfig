using System;

namespace ExistAll.SimpleConfig
{
	public struct ConfigBindingContext
	{
		public string Section { get; }
		public string Key { get; }
		public string CurrentValue { get; internal set; }

		public ConfigBindingContext(string section, string key)
		{
			if (string.Equals(section, null, StringComparison.Ordinal)) throw new ArgumentNullException(nameof(section));
			if (string.Equals(key, null, StringComparison.Ordinal)) throw new ArgumentNullException(nameof(key));

			Section = section;
			Key = key;
			CurrentValue = null;
		}
	}
}
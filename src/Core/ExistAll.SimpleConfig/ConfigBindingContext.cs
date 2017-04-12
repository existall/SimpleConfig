using System;

namespace ExistAll.SimpleConfig
{
	public struct ConfigBindingContext
	{
		public string Section { get; }
		public string Key { get; }
		public string CurrentValue { get; internal set; }

		public ConfigBindingContext(string section, string key, string value)
		{
			if (section == null) throw new ArgumentNullException(nameof(section));
			if (key == null) throw new ArgumentNullException(nameof(key));

			Section = section;
			Key = key;
			CurrentValue = value;
		}
	}
}
using System;

namespace ExistAll.Settings
{
	public struct SettingsBindingContext
	{
		public string Section { get; }
		public string Key { get; }
		public string CurrentValue { get; }

		public SettingsBindingContext(string section, string key, string value)
		{
			if (section == null) throw new ArgumentNullException(nameof(section));
			if (key == null) throw new ArgumentNullException(nameof(key));

			Section = section;
			Key = key;
			CurrentValue = value;
		}
	}
}
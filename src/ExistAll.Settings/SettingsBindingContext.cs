using System;

namespace ExistAll.Settings
{
	public class SettingsBindingContext
	{
		public string Section { get; }
		public string Key { get; }
		public object Value { get; set; }

		public SettingsBindingContext(string section, string key, object value)
		{
			if (section == null) throw new ArgumentNullException(nameof(section));
			if (key == null) throw new ArgumentNullException(nameof(key));

			Section = section;
			Key = key;
			Value = value;
		}
	}
}
using System;
using System.Reflection;

namespace ExistAll.SimpleConfig
{
	public class BindingContext
	{
		internal object NewValue { get; private set; }

		internal bool HasNewValue { get; private set; }
		
		public Type ConfigType { get; }
		
		public Type PropertyType { get; }
		
		public PropertyInfo PropertyInfo { get; }
		
		public string Section { get; }
		public string Key { get; }

		public object CurrentValue { get; }

		public BindingContext(string section,
			string key,
			Type configType,
			PropertyInfo propertyInfo,
			object currentValue)
		{
			if (section == null) throw new ArgumentNullException(nameof(section));
			if (key == null) throw new ArgumentNullException(nameof(key));
			if (string.Equals(section, null, StringComparison.Ordinal)) throw new ArgumentNullException(nameof(section));
			if (string.Equals(key, null, StringComparison.Ordinal)) throw new ArgumentNullException(nameof(key));

			Section = section;
			Key = key;
			ConfigType = configType ?? throw new ArgumentNullException(nameof(configType));
			PropertyInfo = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
			PropertyType = propertyInfo.DeclaringType;
			CurrentValue = currentValue;
		}
		
		public void SetNewValue(object newValue)
		{
			HasNewValue = true;
			NewValue = newValue;
		}
	}
}
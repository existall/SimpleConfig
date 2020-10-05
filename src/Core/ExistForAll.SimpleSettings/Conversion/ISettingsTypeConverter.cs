using System;

namespace ExistForAll.SimpleSettings.Conversion
{
	public interface ISettingsTypeConverter
	{
		bool CanConvert(Type settingsType);
		object Convert(object value, Type settingsType);
	}
}
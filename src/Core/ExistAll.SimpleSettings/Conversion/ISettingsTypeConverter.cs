using System;

namespace ExistAll.SimpleSettings.Conversion
{
	public interface ISettingsTypeConverter
	{
		bool CanConvert(Type settingsType);
		object Convert(object value, Type settingsType);
	}
}
namespace ExistAll.Settings.Core
{
	internal interface ISettingOptionsValidator
	{
		void ValidateOptions(SettingsOptions settingsOptions);
	}

	internal class SettingOptionsValidator : ISettingOptionsValidator
	{
		public void ValidateOptions(SettingsOptions settingsOptions)
		{
			if (settingsOptions.AttributeType == null &&
				settingsOptions.InterfaceBase == null &&
				string.IsNullOrWhiteSpace(settingsOptions.SettingSufix))
			{
				throw new SettingsOptionsArgumentNullException();
			}

			if(string.IsNullOrWhiteSpace(settingsOptions.ArraySplitDelimiter))
				throw new SettingsOptionsArgumentMissingException(nameof(settingsOptions.ArraySplitDelimiter));

			if(string.IsNullOrWhiteSpace(settingsOptions.DateTimeFormat))
				throw new SettingsOptionsArgumentMissingException(nameof(settingsOptions.DateTimeFormat));
		}
	}
}
using System;

namespace ExistForAll.SimpleSettings
{
	public class SettingsOptionsArgumentMissingException : Exception
	{
		public SettingsOptionsArgumentMissingException(string argumentName)
			: base(Resources.SettingsOptionsArgumentMissingMessage(argumentName))
		{
		}
	}
}
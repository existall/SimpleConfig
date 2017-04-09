using System;

namespace ExistAll.Settings
{
	public class SettingsOptionsArgumentMissingException : Exception
	{
		public SettingsOptionsArgumentMissingException(string argumentName)
			: base(Resources.SettingsOptionsArgumentMissingMessage(argumentName))
		{
		}
	}
}
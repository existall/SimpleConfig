using System;

namespace ExistAll.SimpleSettings
{
	public class SettingsOptionsArgumentMissingException : Exception
	{
		public SettingsOptionsArgumentMissingException(string argumentName)
			: base(Resources.SettingsOptionsArgumentMissingMessage(argumentName))
		{
		}
	}
}
using System;

namespace ExistForAll.SimpleSettings
{
	public class SettingsOptionsArgumentNullException : Exception
	{
		public SettingsOptionsArgumentNullException() : base(Resources.SettingsOptionsArgumentNullMessage)
		{
		}
	}
}
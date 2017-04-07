using System;

namespace ExistAll.Settings
{
	public class SettingsOptionsArgumentNullException : Exception
	{
		public SettingsOptionsArgumentNullException() : base(Resources.SettingsOptionsArgumentNullMessage)
		{
		}
	}
}
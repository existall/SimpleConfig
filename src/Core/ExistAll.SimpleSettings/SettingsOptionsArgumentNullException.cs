using System;

namespace ExistAll.SimpleSettings
{
	public class SettingsOptionsArgumentNullException : Exception
	{
		public SettingsOptionsArgumentNullException() : base(Resources.SettingsOptionsArgumentNullMessage)
		{
		}
	}
}
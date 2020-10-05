using System;

namespace ExistForAll.SimpleSettings.Core.Reflection
{
	internal class SettingsPropertyExtractionException : Exception
	{
		public SettingsPropertyExtractionException(Type type, Exception exception) :
			base(Resources.SettingsPropertiesExtractionMessage(type), exception)
		{

		}
	}
}
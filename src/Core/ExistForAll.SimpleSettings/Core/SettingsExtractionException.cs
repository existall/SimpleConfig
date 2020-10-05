using System;

namespace ExistForAll.SimpleSettings.Core
{
	public class SettingsExtractionException : Exception
	{
		public SettingsExtractionException(Type type, Exception e)
			: base(Resources.SettingsExtractionsExceptionMessage(type),e)
		{

		}
	}
}
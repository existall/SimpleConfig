using System;

namespace ExistAll.SimpleConfig.Core
{
	public class ConfigExtractionException : Exception
	{
		public ConfigExtractionException(Type type, Exception e)
			: base(Resources.ConfigExtractionsExceptionMessage(type),e)
		{

		}
	}
}
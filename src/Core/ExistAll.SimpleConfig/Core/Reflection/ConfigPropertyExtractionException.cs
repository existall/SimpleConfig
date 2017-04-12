using System;

namespace ExistAll.SimpleConfig.Core.Reflection
{
	internal class ConfigPropertyExtractionException : Exception
	{
		public ConfigPropertyExtractionException(Type type, Exception exception) :
			base(Resources.ConfigPropertiesExtractionMessage(type), exception)
		{

		}
	}
}
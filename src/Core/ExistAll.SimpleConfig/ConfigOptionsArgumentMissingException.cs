using System;

namespace ExistAll.SimpleConfig
{
	public class ConfigOptionsArgumentMissingException : Exception
	{
		public ConfigOptionsArgumentMissingException(string argumentName)
			: base(Resources.ConfigOptionsArgumentMissingMessage(argumentName))
		{
		}
	}
}
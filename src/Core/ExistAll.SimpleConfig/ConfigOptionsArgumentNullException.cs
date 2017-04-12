using System;

namespace ExistAll.SimpleConfig
{
	public class ConfigOptionsArgumentNullException : Exception
	{
		public ConfigOptionsArgumentNullException() : base(Resources.ConfigOptionsArgumentNullMessage)
		{
		}
	}
}
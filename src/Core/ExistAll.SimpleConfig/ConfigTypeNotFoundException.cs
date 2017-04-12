using System;

namespace ExistAll.SimpleConfig
{
	public class ConfigTypeNotFoundException : Exception
	{
		public ConfigTypeNotFoundException(Type configType)
			:base(Resources.GetConfigNotFoundMessageFormatMessage(configType))
		{
		}
	}
}
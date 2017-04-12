using System;

namespace ExistAll.SimpleConfig
{
	public class ConfigHolder : IConfigHolder
	{
		public ConfigHolder(Type configType, object configImplementation)
		{
			ConfigType = configType;
			ConfigImplementation = configImplementation;
		}

		public Type ConfigType { get; }
		public object ConfigImplementation { get; }
	}
}

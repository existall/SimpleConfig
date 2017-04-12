using System;

namespace ExistAll.SimpleConfig
{
	internal interface IConfigHolder
	{
		Type ConfigType { get; }
		object ConfigImplementation { get; }
	}
}
using System;

namespace ExistAll.SimpleConfig.Core.Reflection
{
	internal interface IConfigClassGenerator
	{
		Type GenerateType(Type interfaceType);
	}
}
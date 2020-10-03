using System;

namespace ExistAll.SimpleSettings.Core.Reflection
{
	internal interface ISettingsClassGenerator
	{
		Type GenerateType(Type interfaceType);
	}
}
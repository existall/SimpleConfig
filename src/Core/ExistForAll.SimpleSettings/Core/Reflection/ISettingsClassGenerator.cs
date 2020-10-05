using System;

namespace ExistForAll.SimpleSettings.Core.Reflection
{
	internal interface ISettingsClassGenerator
	{
		Type GenerateType(Type interfaceType);
	}
}
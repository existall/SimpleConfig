using System;

namespace ExistAll.Settings.Core.Reflection
{
	internal interface ISettingsClassGenerator
	{
		Type GenerateType(Type interfaceType);
	}
}
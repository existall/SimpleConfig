using System;

namespace ExistAll.SimpleSettings.Core.Reflection
{
	internal class TypeGenerationException : Exception
	{
		public TypeGenerationException(Type type, Exception e) :
			base(Resources.SettingsClassGenerationException(type),e)
		{

		}
	}
}
using System;

namespace ExistAll.Settings.Core.Reflection
{
	internal class TypeGenerationException : Exception
	{
		public TypeGenerationException(Type type, Exception e) :
			base(Resources.SettingsClassGenerationException(type),e)
		{

		}
	}
}
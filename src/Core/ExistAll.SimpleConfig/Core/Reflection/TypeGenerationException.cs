using System;

namespace ExistAll.SimpleConfig.Core.Reflection
{
	internal class TypeGenerationException : Exception
	{
		public TypeGenerationException(Type type, Exception e) :
			base(Resources.ConfigClassGenerationException(type),e)
		{

		}
	}
}
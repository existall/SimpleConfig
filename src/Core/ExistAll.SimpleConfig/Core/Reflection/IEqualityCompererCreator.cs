using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace ExistAll.SimpleConfig.Core.Reflection
{
	internal interface IEqualityCompererCreator
	{
		void CreateEqualsMethod(TypeBuilder typeBuilder, List<FieldInfo> fields);
		void CreateGetHashCodeMethod(TypeBuilder typeBuilder, List<FieldInfo> fields);
	}
}
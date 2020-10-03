using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace ExistAll.SimpleSettings.Core.Reflection
{
	internal interface IPropertyCreator
	{
		void CreateAnonymousProperties(TypeBuilder typeBuilder, PropertyInfo[] properties, out List<FieldInfo> fields);
	}
}
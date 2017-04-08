﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ExistAll.Settings.Core.Reflection
{
    internal class PropertyCreator : IPropertyCreator
	{
		public void CreateAnonymousProperties(TypeBuilder typeBuilder, PropertyInfo[] properties, out List<FieldInfo> fields)
		{
			if (typeBuilder == null) throw new ArgumentNullException(nameof(typeBuilder));
			if (properties == null) throw new ArgumentNullException(nameof(properties));

			fields = new List<FieldInfo>(properties.Count());

			foreach (var propertyInfo in properties)
			{
				var field = typeBuilder.DefineField($"_{propertyInfo.Name}", propertyInfo.PropertyType, FieldAttributes.Private);
				var property = typeBuilder.DefineProperty(propertyInfo.Name, PropertyAttributes.HasDefault, propertyInfo.PropertyType, null);
				var getter = typeBuilder.DefineMethod($"get_{property.Name}", MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig | MethodAttributes.Virtual,  null, new[] { property.PropertyType });
				var setter = typeBuilder.DefineMethod($"set_{property.Name}", MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig | MethodAttributes.Virtual, null, new[] { property.PropertyType });

				var getterGenerator = getter.GetILGenerator();
				getterGenerator.Emit(OpCodes.Ldarg_0);
				getterGenerator.Emit(OpCodes.Ldfld, field);
				getterGenerator.Emit(OpCodes.Ret);

				var setterGenerator = setter.GetILGenerator();
				setterGenerator.Emit(OpCodes.Ldarg_0);
				setterGenerator.Emit(OpCodes.Ldarg_1);
				setterGenerator.Emit(OpCodes.Stfld, field);
				setterGenerator.Emit(OpCodes.Ret);

				property.SetGetMethod(getter);
				property.SetSetMethod(setter);

				fields.Add(field);

				//typeBuilder.DefineMethodOverride();
			}
		}
	}

	internal interface IPropertyCreator
	{
		void CreateAnonymousProperties(TypeBuilder typeBuilder, PropertyInfo[] properties, out List<FieldInfo> fields);
	}
}

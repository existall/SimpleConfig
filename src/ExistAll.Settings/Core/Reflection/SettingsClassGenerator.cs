using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ExistAll.Settings.Core.Reflection
{
	internal class SettingsClassGenerator : ISettingsClassGenerator
	{
		private readonly ITypePropertiesExtractor _typePropertiesExtractor;
		private readonly IPropertyCreator _propertyCreator;
		private readonly IEqualityCompererCreator _equalityCompererCreator;
		private readonly ModuleBuilder _moduleBuilder;

		internal SettingsClassGenerator(ITypePropertiesExtractor typePropertiesExtractor,
			IPropertyCreator propertyCreator,
			IEqualityCompererCreator equalityCompererCreator)
		{
			_typePropertiesExtractor = typePropertiesExtractor;
			_propertyCreator = propertyCreator;
			_equalityCompererCreator = equalityCompererCreator;
		}

		public SettingsClassGenerator()
			: this(new TypePropertiesExtractor(),
				  new PropertyCreator(),
				  new EqualityCompererCreator())
		{
			var assemblyName = new AssemblyName(Guid.NewGuid().ToString());
#if NET451
			var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);
#else
			var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);
#endif


#if ENABLE_LINQ_PARTIAL_TRUST
        new ReflectionPermission(PermissionState.Unrestricted).Assert();
#endif
			try
			{
				_moduleBuilder = assemblyBuilder.DefineDynamicModule("SeetingsModule");
			}
			finally
			{
#if ENABLE_LINQ_PARTIAL_TRUST
            PermissionSet.RevertAssert();
#endif
			}
		}

		public Type GenerateType(Type interfaceType)
		{
			try
			{
				var name = interfaceType.Name.TrimStart('I') + "Impl";

				var existingType = _moduleBuilder.Assembly.GetType(name.Replace("+", "\\+"));
				if (existingType != null)
					return existingType;


				var properties = _typePropertiesExtractor.ExtractTypeProperties(interfaceType);

				var typeBuilder = _moduleBuilder.DefineType(name, TypeAttributes.Class | TypeAttributes.Public);
				typeBuilder.AddInterfaceImplementation(interfaceType);
				List<FieldInfo> fields;
				_propertyCreator.CreateAnonymousProperties(typeBuilder, properties.ToArray(), out fields);
				_equalityCompererCreator.CreateEqualsMethod(typeBuilder, fields);
				_equalityCompererCreator.CreateGetHashCodeMethod(typeBuilder, fields);
				var result = typeBuilder.CreateTypeInfo();
				return result.AsType();
			}
			catch (Exception e)
			{
				throw new TypeGenerationException(interfaceType,e);
			}
		}
	}
}
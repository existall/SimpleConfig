using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ExistAll.SimpleConfig.Core.Reflection
{
	internal class ConfigClassGenerator : IConfigClassGenerator
	{
		private readonly ITypePropertiesExtractor _typePropertiesExtractor;
		private readonly IPropertyCreator _propertyCreator;
		private readonly ModuleBuilder _moduleBuilder;

		internal ConfigClassGenerator(ITypePropertiesExtractor typePropertiesExtractor,
			IPropertyCreator propertyCreator)
		{
			_typePropertiesExtractor = typePropertiesExtractor;
			_propertyCreator = propertyCreator;
		}

		public ConfigClassGenerator()
			: this(new TypePropertiesExtractor(),
				new PropertyCreator())
		{
			var assemblyName = new AssemblyName(Guid.NewGuid().ToString());
#if NET451
			var assemblyBuilder =
AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);
#else
			var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);
#endif


#if ENABLE_LINQ_PARTIAL_TRUST
        new ReflectionPermission(PermissionState.Unrestricted).Assert();
#endif
			try
			{
				_moduleBuilder = assemblyBuilder.DefineDynamicModule("ConfingModule");
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
				var name = $"{interfaceType.GetNormalizeInterfaceName()}Impl";

				var existingType = _moduleBuilder.Assembly.GetType(name.Replace("+", "\\+"));

				if (existingType != null)
					return existingType;

				var properties = _typePropertiesExtractor.ExtractTypeProperties(interfaceType);

				var typeBuilder = _moduleBuilder.DefineType(name, TypeAttributes.Class | TypeAttributes.Public);

				typeBuilder.AddInterfaceImplementation(interfaceType);

				_propertyCreator.CreateAnonymousProperties(typeBuilder, properties.ToArray(), out _);
				
				var result = typeBuilder.CreateTypeInfo();

				return result.AsType();
			}
			catch (Exception e)
			{
				throw new TypeGenerationException(interfaceType, e);
			}
		}
	}
}
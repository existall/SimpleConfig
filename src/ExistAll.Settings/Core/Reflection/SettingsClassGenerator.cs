using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;

namespace ExistAll.Settings.Core.Reflection
{
    internal class SettingsClassGenerator
    {
	    private readonly ITypePropertiesExtractor _typePropertiesExtractor;
	    private readonly IPropertyCreator _propertyCreator;
	    private readonly ModuleBuilder _moduleBuilder;

		internal SettingsClassGenerator(ITypePropertiesExtractor typePropertiesExtractor, IPropertyCreator propertyCreator)
		{
			_typePropertiesExtractor = typePropertiesExtractor;
			_propertyCreator = propertyCreator;
		}

		public SettingsClassGenerator()
			: this(new TypePropertiesExtractor(), new PropertyCreator())
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
				_moduleBuilder = assemblyBuilder.DefineDynamicModule("SomeDll.DynamicModule");
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
			var name =  interfaceType.FullName.TrimStart('I') + "Impl";

			var existingType = _moduleBuilder.Assembly.GetType(name.Replace("+", "\\+"));
			if (existingType != null)
				return existingType;


		    var properties = _typePropertiesExtractor.ExtractTypeProperties(interfaceType);

			var typeBuilder = _moduleBuilder.DefineType(name, TypeAttributes.Class | TypeAttributes.Public);
			typeBuilder.AddInterfaceImplementation(interfaceType);
			List<FieldInfo> fields;
			_propertyCreator.CreateAnonymousProperties(typeBuilder, properties.ToArray(), out fields);

			this.CreateEqualsMethod(typeBuilder, fields);
			this.CreateGetHashCodeMethod(typeBuilder, fields);
			var result = typeBuilder.CreateTypeInfo();
			this.classes.TryAdd(signature, result.AsType());
			Interlocked.CompareExchange(ref this.classCount, this.classes.Count, this.classes.Count);
			return result.AsType();

			return null;
	    }
    }



}

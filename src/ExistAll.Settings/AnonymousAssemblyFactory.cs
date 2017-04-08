using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Loader;
using System.Threading;

namespace ExistAll.Settings
{
	public abstract class AnonymousClass
	{

	}

	public class AnonymousClassSignature : IEquatable<AnonymousClassSignature>
	{
		private AnonymousProperty[] properties;
		private int hashCode = 0;

		public AnonymousClassSignature(IEnumerable<AnonymousProperty> properties)
		{
			if (properties == null) throw new ArgumentNullException(nameof(properties));

			this.properties = properties.ToArray();
			foreach (var property in this.properties)
			{
				this.hashCode = this.hashCode ^ property.Name.GetHashCode() ^ property.Type.GetHashCode();
			}
		}

		public override int GetHashCode() => this.hashCode;

		public override bool Equals(object obj) => obj is AnonymousClassSignature ? this.Equals(obj as AnonymousClassSignature) : false;

		public bool Equals(AnonymousClassSignature other)
		{
			if (other == null) throw new ArgumentNullException(nameof(other));
			if (this.properties.Length != other.properties.Length) return false;

			for (var i = 0; i < this.properties.Length; i++)
			{
				if (this.properties[i].Name != other.properties[i].Name || this.properties[i].Type != other.properties[i].Type)
					return false;
			}

			return true;
		}
	}

	public class AnonymousProperty
	{
		private string name;
		private Type type;

		public AnonymousProperty(string name, Type type)
		{
			if (String.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
			if (type == default(Type)) throw new ArgumentNullException(nameof(type));

			this.name = name;
			this.type = type;
		}

		public string Name => this.name;

		public Type Type => this.type;
	}

	internal interface IAnonymousAssemblyFactory
	{
		Type GetAnonymousClass(IEnumerable<AnonymousProperty> properties);
		void CreateAnonymousProperties(TypeBuilder typeBuilder, List<AnonymousProperty> properties, out List<FieldInfo> fields);
		void CreateEqualsMethod(TypeBuilder typeBuilder, List<FieldInfo> fields);
		void CreateGetHashCodeMethod(TypeBuilder typeBuilder, List<FieldInfo> fields);
	}

	internal class AnonymousAssemblyFactory : IAnonymousAssemblyFactory
	{
		private static readonly Lazy<AnonymousAssemblyFactory> Lazy = new Lazy<AnonymousAssemblyFactory>(() => new AnonymousAssemblyFactory(), LazyThreadSafetyMode.ExecutionAndPublication);

		private readonly ModuleBuilder _moduleBinder;
		private ConcurrentDictionary<AnonymousClassSignature, Type> classes;
		private long classCount;
		private ReaderWriterLockSlim readerWriterLockSlim;

		private AnonymousAssemblyFactory()
		{
			var assemblyName = new AssemblyName("SomeDll.AnonymousClasses");
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
				_moduleBinder = assemblyBuilder.DefineDynamicModule("SomeDll.DynamicModule");
			}
			finally
			{
#if ENABLE_LINQ_PARTIAL_TRUST
            PermissionSet.RevertAssert();
#endif
			}

			this.classes = new ConcurrentDictionary<AnonymousClassSignature, Type>();
			this.classCount = 1;
			this.readerWriterLockSlim = new ReaderWriterLockSlim();
		}

		public Type GetAnonymousClass(IEnumerable<AnonymousProperty> properties)
		{
			if (properties == null) throw new ArgumentNullException(nameof(properties));

			this.readerWriterLockSlim.EnterUpgradeableReadLock();

			try
			{
				var signature = new AnonymousClassSignature(properties);
				Type outType;

				if (!this.classes.TryGetValue(signature, out outType))
				{
#if ENABLE_LINQ_PARTIAL_TRUST
        new ReflectionPermission(PermissionState.Unrestricted).Assert();
#endif
					this.readerWriterLockSlim.EnterWriteLock();
					if (!this.classes.TryGetValue(signature, out outType))
					{
						var className = $"SomeDll_AnonymousClass_{this.classCount++}";
						var typeBuilder = _moduleBinder.DefineType(className, TypeAttributes.Class | TypeAttributes.Public, typeof(AnonymousClass));
						List<FieldInfo> fields;
						this.CreateAnonymousProperties(typeBuilder, properties.ToList(), out fields);
						this.CreateEqualsMethod(typeBuilder, fields);
						this.CreateGetHashCodeMethod(typeBuilder, fields);
						var result = typeBuilder.CreateTypeInfo();
						this.classes.TryAdd(signature, result.AsType());
						Interlocked.CompareExchange(ref this.classCount, this.classes.Count, this.classes.Count);
						return result.AsType();
					}

					return outType;
				}
				else
					return outType;
			}
			finally
			{
#if ENABLE_LINQ_PARTIAL_TRUST
            PermissionSet.RevertAssert();
#endif

				if (this.readerWriterLockSlim.IsWriteLockHeld)
				{
					this.readerWriterLockSlim.ExitWriteLock();
				}

				this.readerWriterLockSlim.ExitUpgradeableReadLock();
			}
		}

		public void CreateAnonymousProperties(TypeBuilder typeBuilder, List<AnonymousProperty> properties, out List<FieldInfo> fields)
		{
			if (typeBuilder == null) throw new ArgumentNullException(nameof(typeBuilder));
			if (properties == null) throw new ArgumentNullException(nameof(properties));

			fields = new List<FieldInfo>(properties.Count);

			foreach (var anonProperty in properties)
			{
				var field = typeBuilder.DefineField($"_{anonProperty.Name}", anonProperty.Type, FieldAttributes.Private);
				var property = typeBuilder.DefineProperty(anonProperty.Name, PropertyAttributes.HasDefault, anonProperty.Type, null);
				var getter = typeBuilder.DefineMethod($"get_{anonProperty.Name}", MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, null, new[] { anonProperty.Type });
				var setter = typeBuilder.DefineMethod($"set_{anonProperty.Name}", MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, null, new[] { anonProperty.Type });

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
			}
		}

		public void CreateEqualsMethod(TypeBuilder typeBuilder, List<FieldInfo> fields)
		{
			if (typeBuilder == null) throw new ArgumentNullException(nameof(typeBuilder));
			if (fields == null) throw new ArgumentNullException(nameof(fields));

			var method = typeBuilder.DefineMethod("Equals", MethodAttributes.Public | MethodAttributes.ReuseSlot | MethodAttributes.Virtual | MethodAttributes.HideBySig, typeof(bool), new[] { typeof(object) });

			var methodGenerator = method.GetILGenerator();
			var other = methodGenerator.DeclareLocal(typeBuilder.AsType());
			var next = methodGenerator.DefineLabel();
			methodGenerator.Emit(OpCodes.Ldarg_1);
			methodGenerator.Emit(OpCodes.Isinst, typeBuilder.AsType());
			methodGenerator.Emit(OpCodes.Stloc, other);
			methodGenerator.Emit(OpCodes.Ldloc, other);
			methodGenerator.Emit(OpCodes.Brtrue_S, next);
			methodGenerator.Emit(OpCodes.Ldc_I4_0);
			methodGenerator.Emit(OpCodes.Ret);
			methodGenerator.MarkLabel(next);
			foreach (var field in fields)
			{
				var comparerType = typeof(EqualityComparer<>).MakeGenericType(field.FieldType).GetTypeInfo();
				next = methodGenerator.DefineLabel();
				methodGenerator.EmitCall(OpCodes.Call, comparerType.GetMethod("get_Default"), null);
				methodGenerator.Emit(OpCodes.Ldarg_0);
				methodGenerator.Emit(OpCodes.Ldfld, field);
				methodGenerator.Emit(OpCodes.Ldloc, other);
				methodGenerator.EmitCall(OpCodes.Callvirt, comparerType.GetMethod("Equals", new[] { field.FieldType, field.FieldType }), null);
				methodGenerator.Emit(OpCodes.Brtrue_S, next);
				methodGenerator.Emit(OpCodes.Ldc_I4);
				methodGenerator.Emit(OpCodes.Ret);
				methodGenerator.MarkLabel(next);
			}

			methodGenerator.Emit(OpCodes.Ldc_I4_1);
			methodGenerator.Emit(OpCodes.Ret);
		}

		public void CreateGetHashCodeMethod(TypeBuilder typeBuilder, List<FieldInfo> fields)
		{
			if (typeBuilder == null) throw new ArgumentNullException(nameof(typeBuilder));
			if (fields == null) throw new ArgumentNullException(nameof(fields));

			var method = typeBuilder.DefineMethod("GetHashCode", MethodAttributes.Public | MethodAttributes.ReuseSlot | MethodAttributes.Virtual | MethodAttributes.HideBySig, typeof(int), null);

			var methodGenerator = method.GetILGenerator();
			methodGenerator.Emit(OpCodes.Ldc_I4_0);
			foreach (var field in fields)
			{
				var comparerType = typeof(EqualityComparer<>).MakeGenericType(field.FieldType).GetTypeInfo();
				methodGenerator.EmitCall(OpCodes.Call, comparerType.GetMethod("get_Default"), null);
				methodGenerator.Emit(OpCodes.Ldarg_0);
				methodGenerator.Emit(OpCodes.Ldfld, field);
				methodGenerator.EmitCall(OpCodes.Callvirt, comparerType.GetMethod("GetHashCode", new[] { field.FieldType }), null);
				methodGenerator.Emit(OpCodes.Xor);
			}

			methodGenerator.Emit(OpCodes.Ret);
		}
	}
}
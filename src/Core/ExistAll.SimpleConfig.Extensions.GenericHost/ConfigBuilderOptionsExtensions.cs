using System;
using System.Linq;
using System.Reflection;
using ExistAll.SimpleConfig.Conversion;

namespace ExistAll.SimpleConfig.Extensions.GenericHost
{
    public static class ConfigBuilderOptionsExtensions
    {
        public static IConfigBuilderOptions SetupOptions(this IConfigBuilderOptions target, Action<ConfigOptions> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            action(target.Options);
            return target;
        }

        public static IConfigBuilderOptions AddConfigTypeConverter(this IConfigBuilderOptions target, IConfigTypeConverter configTypeConverter)
        {
            if (configTypeConverter == null) throw new ArgumentNullException(nameof(configTypeConverter));
            target.AddTypeConverter(configTypeConverter);
            return target;
        }
        
        public static IConfigBuilderOptions SetConfigSuffix(this ConfigBuilderOptions target, string suffix)
        {
            target.SetupOptions(x => x.ConfigSuffix = suffix);
            return target;
        }

        public static IConfigBuilderOptions SetArraySplitDelimiter(this ConfigBuilderOptions target, string arrayDelimiter)
        {
            if (arrayDelimiter == null) throw new ArgumentNullException(nameof(arrayDelimiter));
            target.SetupOptions(x => x.ArraySplitDelimiter = arrayDelimiter);
            return target;
        }

        public static IConfigBuilderOptions SetAttributeType(this ConfigBuilderOptions target, Type attribute)
        {
            if (attribute == null) throw new ArgumentNullException(nameof(attribute));
            target.SetupOptions(x => x.AttributeType = attribute);
            return target;
        }

        public static IConfigBuilderOptions SetDateTimeFormat(this ConfigBuilderOptions target, string dateTimeFormat)
        {
            target.SetupOptions(x => x.DateTimeFormat = dateTimeFormat);
            return target;
        }

        public static IConfigBuilderOptions SetInterfaceBase(this ConfigBuilderOptions target, Type interfaceType)
        {
            target.SetupOptions(x => x.InterfaceBase = interfaceType);
            return target;
        }

        public static ConfigBuilderOptions SetSectionNameFormatter(this ConfigBuilderOptions target, Func<Type, string> sectionNameFormatter)
        {
            target.SetupOptions(x => x.SectionNameFormatter = sectionNameFormatter);
            return target;
        }

        public static IConfigBuilderOptions AddAssembly(this ConfigBuilderOptions target, Assembly assembly)
        {
            target.AddAssemblies(new []{assembly});
            return target;
        }

        public static IConfigBuilderOptions AddAssembly<T>(this ConfigBuilderOptions target)
        {
            var type = typeof(T);
            target.AddAssembly(type.Assembly);
            return target;
        }
        
        public static IConfigBuilderOptions AddAssemblies(this ConfigBuilderOptions target, Assembly assembly, params Assembly[] assemblies)
        {
            assemblies = assemblies == null ? new [] {assembly} : assemblies.Concat(new []{ assembly }).ToArray();
            target.AddAssemblies(assemblies);
            return target;
        }
    }
}
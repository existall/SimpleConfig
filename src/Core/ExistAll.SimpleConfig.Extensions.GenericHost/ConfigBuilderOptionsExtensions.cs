using System;
using System.Linq;
using System.Reflection;

namespace ExistAll.SimpleConfig.Extensions.GenericHost
{
    public static class ConfigBuilderOptionsExtensions
    {
        public static ConfigBuilderOptions SetConfigSuffix(this ConfigBuilderOptions target, string suffix)
        {
            target.SetupOptions(x => x.ConfigSuffix = suffix);
            return target;
        }

        public static ConfigBuilderOptions SetArraySplitDelimiter(this ConfigBuilderOptions target, string arrayDelimiter)
        {
            if (arrayDelimiter == null) throw new ArgumentNullException(nameof(arrayDelimiter));
            target.SetupOptions(x => x.ArraySplitDelimiter = arrayDelimiter);
            return target;
        }

        public static ConfigBuilderOptions SetAttributeType(this ConfigBuilderOptions target, Type attribute)
        {
            if (attribute == null) throw new ArgumentNullException(nameof(attribute));
            target.SetupOptions(x => x.AttributeType = attribute);
            return target;
        }

        public static ConfigBuilderOptions SetDateTimeFormat(this ConfigBuilderOptions target, string dateTimeFormat)
        {
            target.SetupOptions(x => x.DateTimeFormat = dateTimeFormat);
            return target;
        }

        public static ConfigBuilderOptions SetInterfaceBase(this ConfigBuilderOptions target, Type interfaceType)
        {
            target.SetupOptions(x => x.InterfaceBase = interfaceType);
            return target;
        }

        public static ConfigBuilderOptions SetSectionNameFormatter(this ConfigBuilderOptions target, Func<Type, string> sectionNameFormatter)
        {
            target.SetupOptions(x => x.SectionNameFormatter = sectionNameFormatter);
            return target;
        }

        public static ConfigBuilderOptions AddAssembly(this ConfigBuilderOptions target, Assembly assembly)
        {
            target.AddAssemblies(new []{assembly});
            return target;
        }

        public static ConfigBuilderOptions AddAssemblies(this ConfigBuilderOptions target, Assembly assembly, params Assembly[] assemblies)
        {
            assemblies = assemblies == null ? new [] {assembly} : assemblies.Concat(new []{ assembly }).ToArray();
            target.AddAssemblies(assemblies);
            return target;
        }
    }
}
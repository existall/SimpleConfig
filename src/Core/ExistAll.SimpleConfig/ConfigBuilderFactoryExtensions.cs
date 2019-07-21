using System;
using ExistAll.SimpleConfig.Binder;
using ExistAll.SimpleConfig.Conversion;

namespace ExistAll.SimpleConfig
{
    public static class ConfigBuilderFactoryExtensions
    {
        public static IConfigBuilderFactory AddTypeConverter(this IConfigBuilderFactory target, IConfigTypeConverter configTypeConverter)
        {
            if (configTypeConverter == null) throw new ArgumentNullException(nameof(configTypeConverter));
            target.Options.Converters.AddFirst(configTypeConverter);
            return target;
        }
        
        public static IConfigBuilderFactory AddInMemoryCollection(this IConfigBuilderFactory target, InMemoryCollection collection)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            target.AddSectionBinder(new InMemoryBinder(collection));
            return target;
        }
        
        public static IConfigBuilderFactory SetupOptions(this IConfigBuilderFactory target, Action<ConfigOptions> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            action(target.Options);
            return target;
        }

        public static IConfigBuilderFactory SetConfigSuffix(this IConfigBuilderFactory target, string suffix)
        {
            target.SetupOptions(x => x.ConfigSuffix = suffix);
            return target;
        }

        public static IConfigBuilderFactory SetArraySplitDelimiter(this IConfigBuilderFactory target, string arrayDelimiter)
        {
            if (arrayDelimiter == null) throw new ArgumentNullException(nameof(arrayDelimiter));
            target.SetupOptions(x => x.ArraySplitDelimiter = arrayDelimiter);
            return target;
        }

        public static IConfigBuilderFactory SetAttributeType(this IConfigBuilderFactory target, Type attribute)
        {
            if (attribute == null) throw new ArgumentNullException(nameof(attribute));
            target.SetupOptions(x => x.AttributeType = attribute);
            return target;
        }

        public static IConfigBuilderFactory SetDateTimeFormat(this IConfigBuilderFactory target, string dateTimeFormat)
        {
            target.SetupOptions(x => x.DateTimeFormat = dateTimeFormat);
            return target;
        }

        public static IConfigBuilderFactory SetInterfaceBase(this IConfigBuilderFactory target, Type interfaceType)
        {
            target.SetupOptions(x => x.InterfaceBase = interfaceType);
            return target;
        }

        public static IConfigBuilderFactory SetSectionNameFormatter(this IConfigBuilderFactory target, Func<Type, string> sectionNameFormatter)
        {
            target.SetupOptions(x => x.SectionNameFormatter = sectionNameFormatter);
            return target;
        }
    }
}
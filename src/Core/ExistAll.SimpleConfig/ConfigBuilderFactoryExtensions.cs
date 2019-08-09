using System;
using ExistAll.SimpleConfig.Binder;
using ExistAll.SimpleConfig.Conversion;

namespace ExistAll.SimpleConfig
{
    public static class ConfigBuilderFactoryExtensions
    {
        public static T AddTypeConverter<T>(this T target, IConfigTypeConverter configTypeConverter) where T : IConfigBuilderFactory
        {
            if (configTypeConverter == null) throw new ArgumentNullException(nameof(configTypeConverter));
            target.Options.Converters.AddFirst(configTypeConverter);
            return target;
        }
        
        public static T AddInMemoryCollection<T>(this T target, InMemoryCollection collection) where T : IConfigBuilderFactory
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            target.AddSectionBinder(new InMemoryBinder(collection));
            return target;
        }
        
        public static T SetupOptions<T>(this T target, Action<ConfigOptions> action) where T : IConfigBuilderFactory
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            action(target.Options);
            return target;
        }

        public static T SetConfigSuffix<T>(this T target, string suffix) where T : IConfigBuilderFactory
        {
            target.SetupOptions(x => x.ConfigSuffix = suffix);
            return target;
        }

        public static T SetArraySplitDelimiter<T>(this T target, string arrayDelimiter) where T : IConfigBuilderFactory
        {
            if (arrayDelimiter == null) throw new ArgumentNullException(nameof(arrayDelimiter));
            target.SetupOptions(x => x.ArraySplitDelimiter = arrayDelimiter);
            return target;
        }

        public static T SetAttributeType<T>(this T target, Type attribute) where T : IConfigBuilderFactory
        {
            if (attribute == null) throw new ArgumentNullException(nameof(attribute));
            target.SetupOptions(x => x.AttributeType = attribute);
            return target;
        }

        public static T SetDateTimeFormat<T>(this T target, string dateTimeFormat) where T : IConfigBuilderFactory
        {
            target.SetupOptions(x => x.DateTimeFormat = dateTimeFormat);
            return target;
        }

        public static T SetInterfaceBase<T>(this T target, Type interfaceType) where T : IConfigBuilderFactory
        {
            target.SetupOptions(x => x.InterfaceBase = interfaceType);
            return target;
        }

        public static T SetSectionNameFormatter<T>(this T target, Func<Type, string> sectionNameFormatter) where T : IConfigBuilderFactory
        {
            target.SetupOptions(x => x.SectionNameFormatter = sectionNameFormatter);
            return target;
        }
    }
}
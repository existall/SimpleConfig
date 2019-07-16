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
        
        public static  IConfigBuilderFactory AddInMemoryCollection(this IConfigBuilderFactory target, InMemoryCollection collection)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            target.AddSectionBinder(new InMemoryBinder(collection));
            return target;
        }
    }
}
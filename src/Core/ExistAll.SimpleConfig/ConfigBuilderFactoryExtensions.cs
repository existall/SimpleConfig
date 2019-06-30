using System;
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
    }
}
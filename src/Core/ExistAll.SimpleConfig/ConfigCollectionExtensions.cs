using System;

namespace ExistAll.SimpleConfig
{
    public static class ConfigCollectionExtensions
    {
        public static T GetConfig<T>(this IConfigCollection target) where T : class
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            return (T) target.GetConfig(typeof(T));
        }
    }
}
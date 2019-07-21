using System;
using System.Linq;
using System.Reflection;
using ExistAll.SimpleConfig.Conversion;

namespace ExistAll.SimpleConfig.Extensions.GenericHost
{
    public static class ConfigBuilderOptionsExtensions
    {
        public static IConfigBuilderOptions AddAssembly(this IConfigBuilderOptions target, Assembly assembly)
        {
            target.AddAssemblies(new []{assembly});
            return target;
        }

        public static IConfigBuilderOptions AddAssembly<T>(this IConfigBuilderOptions target)
        {
            var type = typeof(T);
            target.AddAssembly(type.Assembly);
            return target;
        }
        
        public static IConfigBuilderOptions AddAssemblies(this IConfigBuilderOptions target, Assembly assembly, params Assembly[] assemblies)
        {
            assemblies = assemblies == null ? new [] {assembly} : assemblies.Concat(new []{ assembly }).ToArray();
            target.AddAssemblies(assemblies);
            return target;
        }
    }
}
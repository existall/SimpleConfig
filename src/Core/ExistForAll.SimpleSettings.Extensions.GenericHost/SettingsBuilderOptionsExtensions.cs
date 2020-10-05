using System.Linq;
using System.Reflection;

namespace ExistForAll.SimpleSettings.Extensions.GenericHost
{
    public static class SettingsBuilderOptionsExtensions
    {
        public static ISettingsBuilderOptions AddAssembly(this ISettingsBuilderOptions target, Assembly assembly)
        {
            target.AddAssemblies(new []{assembly});
            return target;
        }

        public static ISettingsBuilderOptions AddAssembly<T>(this ISettingsBuilderOptions target)
        {
            var type = typeof(T);
            target.AddAssembly(type.Assembly);
            return target;
        }
        
        public static ISettingsBuilderOptions AddAssemblies(this ISettingsBuilderOptions target, Assembly assembly, params Assembly[] assemblies)
        {
            assemblies = assemblies == null ? new [] {assembly} : assemblies.Concat(new []{ assembly }).ToArray();
            target.AddAssemblies(assemblies);
            return target;
        }
    }
}
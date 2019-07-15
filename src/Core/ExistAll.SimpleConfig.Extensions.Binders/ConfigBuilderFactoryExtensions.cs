using System;
using Microsoft.Extensions.Configuration;

namespace ExistAll.SimpleConfig.Binders
{
    public static class ConfigBuilderFactoryExtensions
    {
        public static IConfigBuilderFactory AddConfiguration(this IConfigBuilderFactory target, IConfiguration configuration)
        {
            target.AddSectionBinder(new ConfigurationBinder(configuration));

            return target;
        }
        
        public static IConfigBuilderFactory AddEnvironmentVariable(this IConfigBuilderFactory target)
        {
            target.AddSectionBinder(new EnvironmentVariableBinder());

            return target;
        }
        
        public static IConfigBuilderFactory AddEnvironmentVariable(this IConfigBuilderFactory target,
            Action<EnvironmentVariableBinderOptions> action)
        {
            var options = new EnvironmentVariableBinderOptions();
            action(options);

            var binder = options.Prefix != null
                ? new EnvironmentVariableBinder(options.Prefix)
                : new EnvironmentVariableBinder();

            if (options.VariableNameFormatter != null)
                binder.VariableNameFormatter = options.VariableNameFormatter;

            target.AddSectionBinder(binder);

            return target;
        }
    }
}
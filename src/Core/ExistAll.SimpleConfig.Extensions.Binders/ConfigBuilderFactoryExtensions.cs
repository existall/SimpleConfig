using System;
using Microsoft.Extensions.Configuration;

namespace ExistAll.SimpleConfig.Binders
{
    public static class ConfigBuilderFactoryExtensions
    {
        public static T AddConfiguration<T>(this T target, IConfiguration configuration) where T : IConfigBuilderFactory
        {
            target.AddSectionBinder(new ConfigurationBinder(configuration));

            return target;
        }

        public static T AddEnvironmentVariable<T>(this T target) where T : IConfigBuilderFactory
        {
            target.AddSectionBinder(new EnvironmentVariableBinder());

            return target;
        }

        public static T AddEnvironmentVariable<T>(this T target,
            Action<EnvironmentVariableBinderOptions> action) where T : IConfigBuilderFactory
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

        public static T AddCommandLine<T>(this T target,
            Action<CommandLineConfigBinderOptions> action = null) where T : IConfigBuilderFactory
        {
            var args = Environment.CommandLine.Trim().Split(' ');

            target.AddArguments(args, action);

            return target;
        }

        public static T AddArguments<T>(this T target,
            string[] args
            , Action<CommandLineConfigBinderOptions> action = null) where T : IConfigBuilderFactory
        {
            var options = new CommandLineConfigBinderOptions();
            action?.Invoke(options);

            target.AddSectionBinder(new CommandLineConfigBinder(args, options));

            return target;
        }
    }
}
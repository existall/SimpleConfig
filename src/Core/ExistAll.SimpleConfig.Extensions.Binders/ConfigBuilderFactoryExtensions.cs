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
        
        public static IConfigBuilderFactory AddCommandLine(this IConfigBuilderFactory target,
            Action<CommandLineConfigBinderOptions> action = null)
        {
            var args = Environment.CommandLine.Trim().Split(' ');
            
            target.AddArguments(args, action);
		    
            return target;
        }
        
        public static IConfigBuilderFactory AddArguments(this IConfigBuilderFactory target,
            string[] args 
            ,Action<CommandLineConfigBinderOptions> action = null)
        {
            var options =new CommandLineConfigBinderOptions();
            action?.Invoke(options);
            
            target.AddSectionBinder(new CommandLineConfigBinder(args, options));
		    
            return target;
        }
    }
}
using System;
using Microsoft.Extensions.DependencyInjection;

namespace ExistAll.SimpleConfig.Extensions.GenericHost
{
    public static class ServicesConfigBuilderExtensions
    {
        public static IServiceCollection AddSimpleConfig(this IServiceCollection services)
        {
            IntegrateSimpleConfig(services);
            return services;
        }

        public static IServiceCollection AddSimpleConfig(this IServiceCollection services,
            Action<ConfigBuilderOptions> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            IntegrateSimpleConfig(services, action);
            return services;
        }

        private static void IntegrateSimpleConfig(this IServiceCollection services,
            Action<ConfigBuilderOptions> action = null)
        {
            ConfigBuilderOptions options = null;

            var configBuilder = ConfigBuilder.CreateBuilder(factory =>
            {
                options = new ConfigBuilderOptions(factory);
                action?.Invoke(options);
            });

            var settingsProvider = configBuilder.ScanAssemblies(options.Assemblies);

            // replace this with a replaceable settings provider to support time based configuration

            foreach (var settings in settingsProvider)
            {
                services.AddSingleton(settings.Key, settings.Value);
            }

            services.AddSingleton<IConfigProvider>(new ConfigProvider(configBuilder));
        }
    }
}
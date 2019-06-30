using System;
using System.Collections.Generic;
using System.Reflection;
using ExistAll.SimpleConfig.Core;
using ExistAll.SimpleConfig.Core.Reflection;

namespace ExistAll.SimpleConfig
{
    public class ConfigBuilder
    {
        private readonly IConfigTypesExtractor _configTypesExtractor;
        private readonly IConfigOptionsValidator _configOptionsValidator;
        private readonly IConfigClassGenerator _configClassGenerator;
        private readonly IValuesPopulator _valuesPopulator;

        private ConfigOptions Options { get; }
        private IEnumerable<ISectionBinder> SectionBinders { get; }

        internal ConfigBuilder(ConfigOptions options,
            IEnumerable<ISectionBinder> sectionBinders)
            : this(options,
                sectionBinders,
                new ConfigTypesExtractor(),
                new ConfigOptionsValidator(),
                new ConfigClassGenerator(),
                new ValuesPopulator())
        { }

        internal ConfigBuilder(ConfigOptions options,
            IEnumerable<ISectionBinder> sectionBinders,
            IConfigTypesExtractor configTypesExtractor,
            IConfigOptionsValidator configOptionsValidator,
            IConfigClassGenerator configClassGenerator,
            IValuesPopulator valuesPopulator)
        {
            Options = options;
            SectionBinders = sectionBinders;
            _configTypesExtractor = configTypesExtractor;
            _configOptionsValidator = configOptionsValidator;
            _configClassGenerator = configClassGenerator;
            _valuesPopulator = valuesPopulator;
        }

        public static ConfigBuilder CreateBuilder(Action<IConfigBuilderFactory> buildAction)
        {
            if (buildAction == null) throw new ArgumentNullException(nameof(buildAction));

            var factory = new ConfigBuilderFactory();

            buildAction(factory);

            return new ConfigBuilder(factory.Options, factory.Binders);
        }

        public static ConfigBuilder CreateBuilder()
        {
            var factory = new ConfigBuilderFactory();

            return new ConfigBuilder(factory.Options, factory.Binders);
        }

        public object GetConfig(Type configType)
        {
            if (configType == null) throw new ArgumentNullException(nameof(configType));

            if (!configType.GetTypeInfo().IsInterface)
            {
                throw new InvalidOperationException(Resources.TypeIsNotInterface(configType.Name));
            }

            return InnerBuild(configType);
        }

        public IConfigCollection ScanAssemblies(IEnumerable<Assembly> assemblies)
        {
            if (assemblies == null) throw new ArgumentNullException(nameof(assemblies));

            var extractedConfigTypes = _configTypesExtractor.ExtractConfigTypes(assemblies, Options);

            return InnerBuild(extractedConfigTypes);
        }

        private IConfigCollection InnerBuild(IEnumerable<Type> interfaces)
        {
            _configOptionsValidator.ValidateOptions(Options);

            var collection = new ConfigCollection();

            foreach (var configInterface in interfaces)
            {
                var instance = InnerBuild(configInterface);

                collection.Add(configInterface, instance);
            }

            return collection;
        }

        internal object InnerBuild(Type @interface)
        {
            var generateType = _configClassGenerator.GenerateType(@interface);

            var instance = Activator.CreateInstance(generateType);

            _valuesPopulator.PopulateInstanceWithValues(instance, @interface, Options, SectionBinders);

            return instance;
        }
    }
}
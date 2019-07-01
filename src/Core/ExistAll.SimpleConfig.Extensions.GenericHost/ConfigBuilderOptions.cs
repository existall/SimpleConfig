using System;
using System.Collections.Generic;
using System.Reflection;
using ExistAll.SimpleConfig.Conversion;

namespace ExistAll.SimpleConfig.Extensions.GenericHost
{
    public class ConfigBuilderOptions
    {
        private readonly IConfigBuilderFactory _configBuilderFactory;
        private readonly List<Assembly> _assemblies = new List<Assembly>();

        public IEnumerable<Assembly> Assemblies => _assemblies;

        public ConfigBuilderOptions(IConfigBuilderFactory configBuilderFactory)
        {
            _configBuilderFactory = configBuilderFactory;
        }

        public ConfigBuilderOptions AddSectionBinder(ISectionBinder sectionBinder)
        {
            _configBuilderFactory.AddSectionBinder(sectionBinder);
            return this;
        }

        public ConfigBuilderOptions AddAssemblies(IEnumerable<Assembly> assemblies)
        {
            _assemblies.AddRange(assemblies);
            return this;
        }

        public ConfigBuilderOptions SetupOptions(Action<ConfigOptions> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            action(_configBuilderFactory.Options);
            return this;
        }

        public ConfigBuilderOptions AddConfigTypeConverter(IConfigTypeConverter configTypeConverter)
        {
            if (configTypeConverter == null) throw new ArgumentNullException(nameof(configTypeConverter));
            _configBuilderFactory.AddTypeConverter(configTypeConverter);
            return this;
        }
    }
}

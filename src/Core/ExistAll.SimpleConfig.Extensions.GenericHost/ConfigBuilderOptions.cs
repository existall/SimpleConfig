using System;
using System.Collections.Generic;
using System.Reflection;
using ExistAll.SimpleConfig.Conversion;

namespace ExistAll.SimpleConfig.Extensions.GenericHost
{
    public interface IConfigBuilderOptions : IConfigBuilderFactory
    {
        void AddAssemblies(IEnumerable<Assembly> assemblies);
    }
    
    public class ConfigBuilderOptions : IConfigBuilderOptions
    {
        private readonly IConfigBuilderFactory _configBuilderFactory;
        private readonly List<Assembly> _assemblies = new List<Assembly>();
        public ConfigOptions Options => _configBuilderFactory.Options;
        public IEnumerable<Assembly> Assemblies => _assemblies;

        public void AddAssemblies(IEnumerable<Assembly> assemblies)
        {
            if (assemblies == null) throw new ArgumentNullException(nameof(assemblies));
            _assemblies.AddRange(assemblies);
        }

        public ConfigBuilderOptions(IConfigBuilderFactory configBuilderFactory)
        {
            _configBuilderFactory = configBuilderFactory;
        }

        public void AddSectionBinder(ISectionBinder sectionBinder)
        {
            _configBuilderFactory.AddSectionBinder(sectionBinder);
        }

       
    }

}

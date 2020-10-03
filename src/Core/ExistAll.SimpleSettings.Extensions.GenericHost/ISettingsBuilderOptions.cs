using System.Collections.Generic;
using System.Reflection;

namespace ExistAll.SimpleSettings.Extensions.GenericHost
{
    public interface ISettingsBuilderOptions : ISettingsBuilderFactory
    {
        void AddAssemblies(IEnumerable<Assembly> assemblies);
    }
}
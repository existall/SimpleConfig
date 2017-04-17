using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistAll.SimpleConfig
{
	public interface IConfigBuilder
	{
		void Add(ISectionBinder sectionBinder);
		IConfigCollection Build(IEnumerable<Assembly> assemblies, ConfigOptions options);
		IConfigCollection Build(IEnumerable<Type> interfaces, ConfigOptions options);
	}
}
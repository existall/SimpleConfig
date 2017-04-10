using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExistAll.Settings.Core.Reflection
{
	internal interface ITypePropertiesExtractor
	{
		IEnumerable<PropertyInfo> ExtractTypeProperties(Type type);
	}

	internal class TypePropertiesExtractor : ITypePropertiesExtractor
	{
		public IEnumerable<PropertyInfo> ExtractTypeProperties(Type type)
		{
			try
			{
				var info = type.GetTypeInfo();
				var properties = info.GetProperties().ToList();
				var inherited = info
					.GetInterfaces()
					.SelectMany(x => x.GetTypeInfo().GetProperties())
					.ToList();

				foreach (var property in inherited.Where(p => properties.All(x => x.Name != p.Name)))
					properties.Add(property);

				return properties;
			}
			catch (Exception e)
			{
				throw new SettingsPropertyExtractionException(type,e);
			}

		}
	}
}
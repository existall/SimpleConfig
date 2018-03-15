using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExistAll.SimpleConfig.Core.Reflection
{
	internal class TypePropertiesExtractor : ITypePropertiesExtractor
	{
		public IEnumerable<PropertyInfo> ExtractTypeProperties(Type type)
		{
			try
			{
				var info = type.GetTypeInfo();
				
				var properties = info.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
					.ToList();

				return properties;
			}
			catch (Exception e)
			{
				throw new ConfigPropertyExtractionException(type,e);
			}

		}
	}
}
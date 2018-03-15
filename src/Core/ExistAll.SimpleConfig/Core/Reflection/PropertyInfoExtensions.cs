using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExistAll.SimpleConfig.Core.Reflection
{
	internal static class PropertyInfoExtensions
	{
		public static object GetDefaultValue(this PropertyInfo property)
		{
			var attributes = property.GetCustomAttributes<ConditionalValueBaseAttribute>().ToArray();

			var conditionalValueBaseAttributes = attributes.Where(x => x.GetType() != typeof(DefaultValueAttribute)).ToArray();

			object value;

			if (TryGetValueFromAttributes(conditionalValueBaseAttributes, out value))
			{
				return value;
			}

			var defaultValueAttribute = attributes.Where(x => x.GetType() == typeof(DefaultValueAttribute)).ToArray();

			if (TryGetValueFromAttributes(defaultValueAttribute, out value))
			{
				return value;
			}

			return null;
		}

		private static bool TryGetValueFromAttributes(IEnumerable<ConditionalValueBaseAttribute> attributes, out object value)
		{
			value = null;

			foreach (var attribute in attributes)
			{
				if (attribute.ShouldUse)
				{
					value = attribute.DefaultValue;
					return true;
				}
					
			}

			return false;
		}
	}
}

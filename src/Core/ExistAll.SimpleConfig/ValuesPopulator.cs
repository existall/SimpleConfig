using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExistAll.SimpleConfig.Core.Reflection;

namespace ExistAll.SimpleConfig
{
	internal class ValuesPopulator : IValuesPopulator
	{
		private readonly ITypePropertiesExtractor _typePropertiesExtractor;
		private readonly ITypeConverter _typeConverter;

		public ValuesPopulator() :
			this(new TypePropertiesExtractor(), new TypeConverter())
		{
		}

		internal ValuesPopulator(ITypePropertiesExtractor typePropertiesExtractor, ITypeConverter typeConverter)
		{
			_typePropertiesExtractor = typePropertiesExtractor;
			_typeConverter = typeConverter;
		}

		public void PopulateInstanceWithValues(object instance, 
			Type config,
			ConfigOptions options,
			IEnumerable<ISectionBinder> binders)
        {
            var sectionBinders = binders as ISectionBinder[] ?? binders.ToArray();
            foreach (var property in _typePropertiesExtractor.ExtractTypeProperties(config))
			{
				var context = new ConfigBindingContext(config.GetSectionName(options), property.GetPropertyName());

				string value = null;
				var hasBinderSetValue = false;
				foreach (var binder in sectionBinders)
				{
					try
					{
						if (!binder.TryGetValue(context, out var tempValue))
							continue;

						hasBinderSetValue = true;
						value = tempValue;
						context.CurrentValue = value;
					}
					catch (Exception e)
					{
						throw new ConfigBindingException(binder, context, e);
					}
				}

				var propertyValue = ConvertPropertyValue(config, value, property, options, hasBinderSetValue);
				property.SetValue(instance, propertyValue);
			}
        }

		private object ConvertPropertyValue(Type configType,
			object value,
			PropertyInfo property,
			ConfigOptions options,
			bool hasBinderSetValue)
		{
			try
			{
				value = GetValueOrDefault(value, property, hasBinderSetValue);

				var propertyValue = _typeConverter.ConvertValue(value, property, options);

				return propertyValue;
			}
			catch (Exception e)
			{
				throw new ConfigPropertyValueException(configType, value, property, e);
			}
		}

		private static object GetValueOrDefault(object value, PropertyInfo property, bool hasBinderSetValue)
		{
			return hasBinderSetValue ? value : property.GetDefaultValue();
		}
	}
}
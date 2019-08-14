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
	            var tempValue = property.GetDefaultValue();
				foreach (var binder in sectionBinders)
				{
					var context = new BindingContext(config.GetSectionName(options),
						property.GetPropertyName(),
						config,
						property,
						tempValue);
					
					try
					{
						binder.BindPropertyConfig(context);
						if (context.HasNewValue)
							tempValue = context.NewValue;
					}
					catch (Exception e)
					{
						throw new ConfigBindingException(binder, context, e);
					}
				}

				var propertyValue = ConvertPropertyValue(config, tempValue, property, options);
				property.SetValue(instance, propertyValue);
			}
        }

		private object ConvertPropertyValue(Type configType,
			object value,
			PropertyInfo property,
			ConfigOptions options)
		{
			try
			{
				var propertyValue = _typeConverter.ConvertValue(value, property, options);

				return propertyValue;
			}
			catch (Exception e)
			{
				throw new ConfigPropertyValueException(configType, value, property, e);
			}
		}
	}
}
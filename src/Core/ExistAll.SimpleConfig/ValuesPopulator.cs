using System;
using System.Collections.Generic;
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

		public void PopulateInstanceWithValues(object instance, Type config, ConfigOptions options, SortedList<int, ISectionBinder> binders)
		{
			foreach (var property in _typePropertiesExtractor.ExtractTypeProperties(config))
			{
				var context = new ConfigBindingContext(options.SectionNameFormater(config.Name), property.Name);

				string value = null;
				var hasBinderSetValue = false;
				foreach (var binder in binders)
				{
					try
					{
						string tempValue = null;

						if (!binder.Value.TryGetValue(context, out tempValue))
							continue;

						hasBinderSetValue = true;
						value = tempValue;
						context.CurrentValue = value;
					}
					catch (Exception e)
					{
						throw new ConfigBindingException(binder.Value, context, e);
					}
				}

				ConvertAndSetPropertyValue(value, property, instance, options, hasBinderSetValue);
			}
		}

		private void ConvertAndSetPropertyValue(object value, PropertyInfo property, object instance, ConfigOptions options,
			bool hasBinderSetValue)
		{
			try
			{
				value = GetValue(value, property, options, hasBinderSetValue);

				var propertyValue = _typeConverter.ConvertValue(value, property.PropertyType, options);

				property.SetValue(instance, propertyValue);
			}
			catch (Exception e)
			{
				throw new ConfigPropertyValueException(value, property, e);
			}
		}

		private object GetValue(object value, PropertyInfo property, ConfigOptions options, bool hasBinderSetValue)
		{
			return hasBinderSetValue ? value : property.GetDefaultValue();
		}
	}
}
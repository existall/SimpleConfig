using System;
using System.Collections.Generic;
using System.Reflection;
using ExistAll.SimpleConfig.Core;
using ExistAll.SimpleConfig.Core.Reflection;

namespace ExistAll.SimpleConfig
{
	public class ConfigBuilder : IConfigBuilder
	{
		private readonly IConfigTypesExtractor _configTypesExtractor;
		private readonly IConfigOptionsValidator _configOptionsValidator;
		private readonly IConfigClassGenerator _configClassGenerator;
		private readonly ITypePropertiesExtractor _typePropertiesExtractor;
		private readonly ITypeConverter _typeConverter;
		private int _counter;
		private readonly SortedList<int, ISectionBinder> _binders = new SortedList<int, ISectionBinder>();

		public ConfigBuilder() : this(new ConfigTypesExtractor(),
			new ConfigOptionsValidator(),
			new ConfigClassGenerator(),
			new TypePropertiesExtractor(),
			new TypeConverter())
		{ }

		internal ConfigBuilder(IConfigTypesExtractor configTypesExtractor,
			IConfigOptionsValidator configOptionsValidator,
			IConfigClassGenerator configClassGenerator,
			ITypePropertiesExtractor typePropertiesExtractor,
			ITypeConverter typeConverter)
		{
			_configTypesExtractor = configTypesExtractor;
			_configOptionsValidator = configOptionsValidator;
			_configClassGenerator = configClassGenerator;
			_typePropertiesExtractor = typePropertiesExtractor;
			_typeConverter = typeConverter;
		}

		public IConfigCollection Build(AssemblyCollection assemblies, ConfigOptions options)
		{
			if (options == null) throw new ArgumentNullException(nameof(options));
			_configOptionsValidator.ValidateOptions(options);
			var configInterfaces = _configTypesExtractor.ExtractConfigTypes(assemblies, options);

			var collection = new ConfigCollection();

			foreach (var configInterface in configInterfaces)
			{
				var generateType = _configClassGenerator.GenerateType(configInterface);

				var instance = Activator.CreateInstance(generateType);

				PopulateInstanceWithValues(instance, configInterface, options);

				collection.Add(configInterface, instance);
			}

			return collection;
		}

		public void Add(ISectionBinder sectionBinder)
		{
			if (sectionBinder == null) throw new ArgumentNullException(nameof(sectionBinder));
			_binders.Add(_counter++, sectionBinder);
		}

		private void ConvertAndSetPropertyValue(string value, PropertyInfo property, object instance, ConfigOptions options)
		{
			try
			{
				var propertyValue = value != null
					? _typeConverter.ConvertValue(value, property.PropertyType, options) :
					property.GetDefaultValue();

				property.SetValue(instance, propertyValue);
			}
			catch (Exception e)
			{
				throw new ConfigPropertyValueException(value, property, e);
			}
		}

		private void PopulateInstanceWithValues(object instance, Type config, ConfigOptions options)
		{
			foreach (var property in _typePropertiesExtractor.ExtractTypeProperties(config))
			{
				var context = new ConfigBindingContext(options.SectionNameFormater(config.Name), property.Name, null);

				string value = null;

				foreach (var binder in _binders)
				{
					try
					{
						string tempValue = null;
						if (binder.Value.TryGetValue(context, out tempValue))
						{
							value = tempValue;
							context.CurrentValue = value;
						}
					}
					catch (Exception e)
					{
						throw new ConfigBindingException(binder.Value, context, e);
					}
				}
				ConvertAndSetPropertyValue(value, property, instance, options);
			}
		}
	}
}
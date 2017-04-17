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

		public IConfigCollection Build(IEnumerable<Assembly> assemblies, ConfigOptions options)
		{
			if (assemblies == null) throw new ArgumentNullException(nameof(assemblies));
			if (options == null) throw new ArgumentNullException(nameof(options));
			var configInterfaces = _configTypesExtractor.ExtractConfigTypes(assemblies, options);
			return InnerBuild(configInterfaces, options);
		}

		public IConfigCollection Build(IEnumerable<Type> interfaces, ConfigOptions options)
		{
			if (interfaces == null) throw new ArgumentNullException(nameof(interfaces));
			if (options == null) throw new ArgumentNullException(nameof(options));
			return InnerBuild(interfaces, options);
		}

		public void Add(ISectionBinder sectionBinder)
		{
			if (sectionBinder == null) throw new ArgumentNullException(nameof(sectionBinder));
			_binders.Add(_counter++, sectionBinder);
		}

		private IConfigCollection InnerBuild(IEnumerable<Type> interfaces, ConfigOptions options)
		{
			_configOptionsValidator.ValidateOptions(options);

			var collection = new ConfigCollection();

			foreach (var configInterface in interfaces)
			{
				var generateType = _configClassGenerator.GenerateType(configInterface);

				var instance = Activator.CreateInstance(generateType);

				PopulateInstanceWithValues(instance, configInterface, options);

				collection.Add(configInterface, instance);
			}

			return collection;
		}

		private void ConvertAndSetPropertyValue(string value, PropertyInfo property, object instance, ConfigOptions options, bool hasBinderSetValue)
		{
			try
			{
				var propertyValue = hasBinderSetValue
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
				var context = new ConfigBindingContext(options.SectionNameFormater(config.Name), property.Name);

				string value = null;
				bool hasBinderSetValue = false;
				foreach (var binder in _binders)
				{
					try
					{
						string tempValue = null;

						if (binder.Value.TryGetValue(context, out tempValue))
						{
							hasBinderSetValue = true;
							value = tempValue;
							context.CurrentValue = value;
						}
					}
					catch (Exception e)
					{
						throw new ConfigBindingException(binder.Value, context, e);
					}
				}
				ConvertAndSetPropertyValue(value, property, instance, options, hasBinderSetValue);
			}
		}
	}
}
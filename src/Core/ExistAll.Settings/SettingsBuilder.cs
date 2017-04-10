using System;
using System.Collections.Generic;
using System.Reflection;
using ExistAll.Settings.Core;
using ExistAll.Settings.Core.Reflection;

namespace ExistAll.Settings
{
	public class SettingsBuilder : ISettingsBuilder
	{
		private readonly ISettingTypesExtractor _settingTypesExtractor;
		private readonly ISettingOptionsValidator _settingOptionsValidator;
		private readonly ISettingsClassGenerator _settingsClassGenerator;
		private readonly ITypePropertiesExtractor _typePropertiesExtractor;
		private readonly ITypeConverter _typeConverter;
		private int _counter;
		private readonly SortedList<int, ISectionBinder> _binders = new SortedList<int, ISectionBinder>();

		public SettingsBuilder() : this(new SettingTypesExtractor(),
			new SettingOptionsValidator(),
			new SettingsClassGenerator(),
			new TypePropertiesExtractor(),
			new TypeConverter())
		{ }

		internal SettingsBuilder(ISettingTypesExtractor settingTypesExtractor,
			ISettingOptionsValidator settingOptionsValidator,
			ISettingsClassGenerator settingsClassGenerator,
			ITypePropertiesExtractor typePropertiesExtractor,
			ITypeConverter typeConverter)
		{
			_settingTypesExtractor = settingTypesExtractor;
			_settingOptionsValidator = settingOptionsValidator;
			_settingsClassGenerator = settingsClassGenerator;
			_typePropertiesExtractor = typePropertiesExtractor;
			_typeConverter = typeConverter;
		}

		public ISettingsCollection Build(AssemblyCollection assemblies, SettingsOptions options)
		{
			if (options == null) throw new ArgumentNullException(nameof(options));
			_settingOptionsValidator.ValidateOptions(options);
			var settingInterfaces = _settingTypesExtractor.ExtractSettingTypes(assemblies, options);

			var collection = new SettingsCollection();

			foreach (var setting in settingInterfaces)
			{
				var generateType = _settingsClassGenerator.GenerateType(setting);

				var instance = Activator.CreateInstance(generateType);

				PopulateInstanceWithValues(instance, setting, options);

				collection.Add(setting, instance);
			}

			return collection;
		}

		public void Add(ISectionBinder sectionBinder)
		{
			if (sectionBinder == null) throw new ArgumentNullException(nameof(sectionBinder));
			_binders.Add(_counter++, sectionBinder);
		}

		private void ConvertAndSetPropertyValue(string value, PropertyInfo property, object instance, SettingsOptions options)
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
				throw new SettingsPropertyValueException(value, property, e);
			}
		}

		private void PopulateInstanceWithValues(object instance, Type setting, SettingsOptions options)
		{
			foreach (var property in _typePropertiesExtractor.ExtractTypeProperties(setting))
			{
				var context = new SettingsBindingContext(options.SectionNameFormater(setting.Name), property.Name, null);

				string value = null;

				foreach (var binder in _binders)
				{
					try
					{
						value = binder.Value.GetValue(context);
						context.CurrentValue = value;
					}
					catch (Exception e)
					{
						throw new SettingsBindingException(binder.Value, context, e);
					}
				}

				ConvertAndSetPropertyValue(value, property, instance, options);

			}
		}
	}
}
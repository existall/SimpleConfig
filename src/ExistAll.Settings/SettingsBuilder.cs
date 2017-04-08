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
		private int _counter = 0;
		private readonly SortedList<int, ISectionBinder> _binders = new SortedList<int, ISectionBinder>();

		public SettingsBuilder() : this(new SettingTypesExtractor(),
			new SettingOptionsValidator(),
			new SettingsClassGenerator())
		{ }

		internal SettingsBuilder(ISettingTypesExtractor settingTypesExtractor,
			ISettingOptionsValidator settingOptionsValidator,
			ISettingsClassGenerator settingsClassGenerator)
		{
			_settingTypesExtractor = settingTypesExtractor;
			_settingOptionsValidator = settingOptionsValidator;
			_settingsClassGenerator = settingsClassGenerator;
		}

		public ISettingsCollection Build(AssemblyCollection assemblies, SettingsOptions options)
		{
			if (options == null) throw new ArgumentNullException(nameof(options));
			_settingOptionsValidator.ValidateOptions(options);
			var settingInterfaces = _settingTypesExtractor.ExtractSettingTypes(assemblies, options);

			SettingsCollection collection = new SettingsCollection();

			foreach (var setting in settingInterfaces)
			{
				// create class here
				var generateType = _settingsClassGenerator.GenerateType(setting);

				foreach (var property in setting.GetTypeInfo().DeclaredProperties)
				{
					// use defaults here first

					var context = new SettingsBindingContext(setting.Name, property.Name, null);  // pass the default

					foreach (var binder in _binders)
					{
						binder.Value.Bind(context);
					}
				}

				// set property here.
				collection.Add(setting,null);
			}


			return collection;
		}

		public void Add(ISectionBinder sectionBinder)
		{
			if (sectionBinder == null) throw new ArgumentNullException(nameof(sectionBinder));
			_binders.Add(_counter++, sectionBinder);
		}
	}
}
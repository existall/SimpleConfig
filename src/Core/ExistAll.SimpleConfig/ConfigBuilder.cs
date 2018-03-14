using System;
using System.Collections.Generic;
using System.Reflection;
using ExistAll.SimpleConfig.Convertion;
using ExistAll.SimpleConfig.Core;
using ExistAll.SimpleConfig.Core.Reflection;

namespace ExistAll.SimpleConfig
{
	public class ConfigBuilder
	{
		private readonly IConfigTypesExtractor _configTypesExtractor;
		private readonly IConfigOptionsValidator _configOptionsValidator;
		private readonly IConfigClassGenerator _configClassGenerator;
		private readonly IValuesPopulator _valuesPopulator;
		private int _counter;
		private readonly SortedList<int, ISectionBinder> _binders = new SortedList<int, ISectionBinder>();

		public ConfigOptions Options { get; } = new ConfigOptions();
		
		public ConfigBuilder() 
			: this(new ConfigTypesExtractor(),
			new ConfigOptionsValidator(),
			new ConfigClassGenerator(),
			 new ValuesPopulator())
		{ }

		internal ConfigBuilder(IConfigTypesExtractor configTypesExtractor,
			IConfigOptionsValidator configOptionsValidator,
			IConfigClassGenerator configClassGenerator,
			IValuesPopulator valuesPopulator)
		{
			_configTypesExtractor = configTypesExtractor;
			_configOptionsValidator = configOptionsValidator;
			_configClassGenerator = configClassGenerator;
			_valuesPopulator = valuesPopulator;
		}

		public IConfigCollection Build(IEnumerable<Assembly> assemblies)
		{
			if (assemblies == null) throw new ArgumentNullException(nameof(assemblies));
			var configInterfaces = _configTypesExtractor.ExtractConfigTypes(assemblies, Options);
			return InnerBuild(configInterfaces, Options);
		}

		public IConfigCollection Build(IEnumerable<Type> interfaces)
		{
			if (interfaces == null) throw new ArgumentNullException(nameof(interfaces));
			return InnerBuild(interfaces, Options);
		}

		public void AddSectionBinder(ISectionBinder sectionBinder)
		{
			if (sectionBinder == null) throw new ArgumentNullException(nameof(sectionBinder));
			_binders.Add(_counter++, sectionBinder);
		}

		public void AddTypeConverter(IConfigTypeConverter configTypeConverter)
		{
			if (configTypeConverter == null) throw new ArgumentNullException(nameof(configTypeConverter));
			Options.Converters.AddFirst(configTypeConverter);
		}
		
		private IConfigCollection InnerBuild(IEnumerable<Type> interfaces, ConfigOptions options)
		{
			_configOptionsValidator.ValidateOptions(options);

			var collection = new ConfigCollection();

			foreach (var configInterface in interfaces)
			{
				var generateType = _configClassGenerator.GenerateType(configInterface);

				var instance = Activator.CreateInstance(generateType);

				_valuesPopulator.PopulateInstanceWithValues(instance, configInterface, options, _binders);

				collection.Add(configInterface, instance);
			}

			return collection;
		}
	}
}
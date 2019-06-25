using System;
using System.Collections.Generic;
using System.Reflection;
using ExistAll.SimpleConfig.Conversion;
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
		private readonly List<Assembly> _assemblies = new List<Assembly>();
		
		public ConfigOptions Options { get; } = new ConfigOptions();

		public static ConfigBuilder CreateBuilder()
		{
			return new ConfigBuilder();
		}
		
		private ConfigBuilder()
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

		public IConfigCollection Build()
		{
			var configInterfaces = _configTypesExtractor.ExtractConfigTypes(_assemblies, Options);
			return InnerBuild(configInterfaces);
		}

		public ConfigBuilder AddSectionBinder(ISectionBinder sectionBinder)
		{
			if (sectionBinder == null) throw new ArgumentNullException(nameof(sectionBinder));
			_binders.Add(_counter++, sectionBinder);
			return this;
		}

		public ConfigBuilder AddAssembly(Assembly assembly)
		{
			if (assembly == null) throw new ArgumentNullException(nameof(assembly));

			_assemblies.Add(assembly);

			return this;
		}
		
		private IConfigCollection InnerBuild(IEnumerable<Type> interfaces)
		{
			_configOptionsValidator.ValidateOptions(Options);

			var collection = new ConfigCollection(this);

			foreach (var configInterface in interfaces)
			{
				var instance = BuildInterface(configInterface);

				collection.Add(configInterface, instance);
			}

			return collection;
		}

		internal object BuildInterface(Type @interface)
		{
			var generateType = _configClassGenerator.GenerateType(@interface);

			var instance = Activator.CreateInstance(generateType);

			_valuesPopulator.PopulateInstanceWithValues(instance, @interface, Options, _binders);

			return instance;
		}
	}
}
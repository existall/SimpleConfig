using System;
using ExistAll.SimpleConfig.Conversion;
using ExistAll.SimpleConfig.Core.Reflection;

namespace ExistAll.SimpleConfig
{
	public class ConfigOptions
	{
		internal TypeConvertersCollections Converters { get; }

		public Type AttributeType { get; set; } = typeof(ConfigSectionAttribute);
		public Type InterfaceBase { get; set; } = typeof(IConfigSection);
		public string ConfigSuffix { get; set; } = "Config";
		public string ArraySplitDelimiter { get; set; } = ",";
		public string DateTimeFormat { get; set; } = "yyyy-MM-dd";
		public Func<Type, string> SectionNameFormater { get; set; } = (interfaceType) => interfaceType.GetNormalizeInterfaceName();

		public ConfigOptions()
		{
			Converters = new TypeConvertersCollections(this);
		}
	}
}
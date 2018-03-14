using System;
using ExistAll.SimpleConfig.Convertion;

namespace ExistAll.SimpleConfig
{
	public class ConfigOptions
	{
		public Type AttributeType { get; set; } = typeof(ConfigSectionAttribute);
		public Type InterfaceBase { get; set; } = typeof(IConfigSection);
		public string ConfigSuffix { get; set; } = "Config";
		public string ArraySplitDelimiter { get; set; } = ",";
		public string DateTimeFormat { get; set; } = "yyyy-MM-dd";
		public Func<string, string> SectionNameFormater { get; set; } = (interfaceName) => interfaceName.Trim('I');
		
		internal TypeConvertersCollections Converters { get; }

		public ConfigOptions()
		{
			Converters = new TypeConvertersCollections(this);
		}
	}
}
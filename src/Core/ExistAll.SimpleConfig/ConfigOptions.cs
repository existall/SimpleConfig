using System;

namespace ExistAll.SimpleConfig
{
	public class ConfigOptions
	{
		public Type AttributeType { get; set; } = typeof(ConfigSectionAttribute);
		public Type InterfaceBase { get; set; } = typeof(IConfigSection);
		public string ConfigSufix { get; set; } = "Config";
		public string ArraySplitDelimiter { get; set; } = ",";
		public string DateTimeFormat { get; set; } = "yyyy-MM-dd";
		public Func<string, string> SectionNameFormater => (interfaceName) => interfaceName.Trim('I');
	}
}
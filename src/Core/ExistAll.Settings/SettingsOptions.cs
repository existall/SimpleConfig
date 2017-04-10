using System;

namespace ExistAll.Settings
{
	public class SettingsOptions
	{
		public Type AttributeType { get; set; } = typeof(SettingsSectionAttribue);
		public Type InterfaceBase { get; set; } = typeof(ISettingSection);
		public string SettingSufix { get; set; } = "Settings";
		public string ArraySplitDelimiter { get; set; } = ",";
		public string DateTimeFormat { get; set; } = "yyyy-MM-dd";
		public Func<string, string> SectionNameFormater => (interfaceName) => interfaceName.Trim('I');
	}
}
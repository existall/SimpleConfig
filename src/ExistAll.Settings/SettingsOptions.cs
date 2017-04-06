using System;

namespace ExistAll.Settings
{
	public class SettingsOptions
	{
		public Type AttributeType { get; set; } = typeof(SettingsSectionAttribue);
		public Type InterfaceBase { get; set; } = typeof(ISettingSection);
		public string SettingSufix { get; set; } = "Settings";
	}
}
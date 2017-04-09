using System;

namespace ExistAll.Settings
{
	internal class Resources
	{
		public const string SettingsOptionsArgumentNullMessage = @"Settings builder must have at least one type of indication to know what is a settings, 
				the options are an attribute, Interface or type name suffix. You can use all of them or one but you need at least one. ExistAll.Settings 
				also provide to override each of the options thus you can provide your own set of attribute or interface. allowing you to decouple this framework
				from you domains.";

		public static string GetSettingsNotFoundMesageFormatMessage(Type settingType)
			=> $@"Setting types [{settingType.Name}] was not found, this could be due to several reasons:
				1. The settings type was not found in the assemblies you provided
				2. Settings indication (Attribute, Interface, Suffix) was not set correctly";
	}
}
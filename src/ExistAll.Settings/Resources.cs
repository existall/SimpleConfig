using System;
using System.Reflection;

namespace ExistAll.Settings
{
	internal class Resources
	{
		public const string SettingsOptionsArgumentNullMessage = @"Settings builder must have at least one type of indication to know what is a settings, 
				the options are an attribute, Interface or type name suffix. You can use all of them or one but you need at least one. ExistAll.Settings 
				also provide to override each of the options thus you can provide your own set of attribute or interface. allowing you to decouple this framework
				from you domains.";

		public static string SettingsOptionsArgumentMissingMessage(string argumentName) => $@"Settings argument [{argumentName}] must be set.";

		public static string GetSettingsNotFoundMessageFormatMessage(Type settingType)
			=> $@"Setting types [{settingType.Name}] was not found, this could be due to several reasons:
				1. The settings type was not found in the assemblies you provided
				2. Settings indication (Attribute, Interface, Suffix) was not set correctly";

		public static string SettingBindingExceptionMessage(ISectionBinder binder, string section,
			string key) => $@"An error has occurred while [{binder.GetType().FullName}] tried
				to bind section [{section}] with key [{key}], if this is custom binding check your implementation of ISectionBinder.
				If this is ExistAll.Setting binder please let us know on github.";

		public static string SettingsExtractionsExceptionMessage(Type type) =>
			$@"An error occurred while trying to get the type [{type.FullName}]
				from it's assembly, we only check if this type has some of our settings indications like attribute, an interface or suffix.
				see inner exception for more details";

		public static string SettingsClassGenerationException(Type type) =>
			$@"While trying to generate a class from interface [{type.FullName}] something went wrong.
				please see inner exception for more details";

		public static string PropertySetterExceptionMessage(string value, PropertyInfo property) =>
			$@"fail to to set the value [{value}] within
				the property [{property.Name}]. see inner exception for more details";

		public static string SettingsPropertiesExtractionMessage(Type type) =>
			$@"An error has occurred while trying to extract
				all properties from type [{type.FullName}]";
	}
}
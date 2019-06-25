using System;
using System.Reflection;

namespace ExistAll.SimpleConfig
{
	internal class Resources
	{
		public const string ConfigOptionsArgumentNullMessage = @"Config builder must have at least one type of indication to know what is a Config, 
				the options are an attribute, Interface or type name suffix. You can use all of them or one but you need at least one. ExistAll.SimpleConfig 
				also provide to override each of the options thus you can provide your own set of attribute or interface. allowing you to decouple this framework
				from you domains.";

		public static string ConfigOptionsArgumentMissingMessage(string argumentName) => $@"config argument [{argumentName}] must be set.";

		public static string GetConfigNotFoundMessageFormatMessage(Type configType)
			=> $@"Config types [{configType.Name}] was not found, this could be due to several reasons:
				1. The config type was not found in the assemblies you provided
				2. config indication (Attribute, Interface, Suffix) was not set correctly";

		public static string ConfigBindingExceptionMessage(ISectionBinder binder, string section,
			string key) => $@"An error has occurred while [{binder.GetType().FullName}] tried
				to bind section [{section}] with key [{key}], if this is custom binding check your implementation of ISectionBinder.
				If this is ExistAll.Config binder please let us know on github.";

		public static string ConfigExtractionsExceptionMessage(Type type) =>
			$@"An error occurred while trying to get the type [{type.FullName}]
				from it's assembly, we only check if this type has some of our config indications like attribute, an interface or suffix.
				see inner exception for more details";

		public static string ConfigClassGenerationException(Type type) =>
			$@"While trying to generate a class from interface [{type.FullName}] something went wrong.
				please see inner exception for more details";

		public static string PropertySetterExceptionMessage(Type interfaceType, object value, PropertyInfo property) =>
			$@"failed to to set the value [{value}] within the property [{property.Name}] for interface [{interfaceType.Name}].
see inner exception for more details";

		public static string ConfigPropertiesExtractionMessage(Type type) =>
			$@"An error has occurred while trying to extract
				all properties from type [{type.FullName}]";

		public static string PropertyNotAllowNullMessage(string propertyName) =>
			$@"[{propertyName}] is marked as Null not allowed, yet the value is null. please provide value via binder or attribute";

		public static string TypeIsNotInterface(string typeName) =>
			@"[{typeName}] is not an interface, SimpleConfig supports only interfaces";
	}
}
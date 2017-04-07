using System;
using System.Linq;
using System.Reflection;

namespace ExistAll.Settings.Core
{
	internal interface ISettingTypesExtractor
	{
		Type[] ExtractSettingTypes(AssemblyCollection assemblies, SettingsOptions options);
	}

	internal class SettingTypesExtractor : ISettingTypesExtractor
	{
		public Type[] ExtractSettingTypes(AssemblyCollection assemblies, SettingsOptions options)
		{
			return assemblies.GetTypes()
				.Where(x => x.GetTypeInfo().IsInterface && IsFromOptions(x, options))
				.ToArray();
		}

		private static bool IsFromOptions(Type type, SettingsOptions options)
		{
			var info = type.GetTypeInfo();

			if (info.GetCustomAttribute(options.AttributeType, true) != null)
				return true;

			if (info.IsAssignableFrom(options.InterfaceBase))
				return true;

			if (info.Name.ToLower().EndsWith(options.SettingSufix.Trim().ToLower()))
				return true;

			return false;
		}
	}
}

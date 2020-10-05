using System;
using ExistForAll.SimpleSettings.Binder;

namespace ExistForAll.SimpleSettings.DotNet.Frameworks
{
    public static class SettingsBuilderFactoryExtensions
    {
        public static ISettingsBuilderFactory AddAppSettings(this ISettingsBuilderFactory target)
        {
            target.AddSectionBinder(new AppSettingsBinder());
		    
            return target;
        }

        public static ISettingsBuilderFactory AddAppSettings(this ISettingsBuilderFactory target, NameFormatter action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            target.AddSectionBinder(new AppSettingsBinder() {VariableNameFormatter = action});

            return target;
        }
    }
}
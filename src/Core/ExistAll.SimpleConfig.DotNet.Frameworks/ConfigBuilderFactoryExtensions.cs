using System;

namespace ExistAll.SimpleConfig.DotNet.Frameworks
{
    public static class ConfigBuilderFactoryExtensions
    {
        public static IConfigBuilderFactory AddAppSettings(this IConfigBuilderFactory target)
        {
            target.AddSectionBinder(new AppSettingsBinder());
		    
            return target;
        }

        public static IConfigBuilderFactory AddAppSettings(this IConfigBuilderFactory target, NameFormatter action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            target.AddSectionBinder(new AppSettingsBinder() {VariableNameFormatter = action});

            return target;
        }
    }
}
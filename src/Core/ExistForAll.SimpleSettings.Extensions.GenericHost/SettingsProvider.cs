using System;

namespace ExistForAll.SimpleSettings.Extensions.GenericHost
{
    internal class SettingsProvider : ISettingsProvider
    {
        private readonly SettingsBuilder _settingsBuilder;

        public SettingsProvider(SettingsBuilder configBuilder)
        {
            _settingsBuilder = configBuilder;
        }

        public object GetSettings(Type type)
        {
            return _settingsBuilder.GetSettings(type);
        }
    }
}
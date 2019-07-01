using System;

namespace ExistAll.SimpleConfig.Extensions.GenericHost
{
    internal class ConfigProvider : IConfigProvider
    {
        private readonly ConfigBuilder _configBuilder;

        public ConfigProvider(ConfigBuilder configBuilder)
        {
            _configBuilder = configBuilder;
        }

        public object GetConfig(Type type)
        {
            return _configBuilder.GetConfig(type);
        }
    }
}
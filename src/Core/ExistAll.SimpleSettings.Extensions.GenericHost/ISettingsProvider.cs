using System;

namespace ExistAll.SimpleSettings.Extensions.GenericHost
{
    public interface ISettingsProvider
    {
        object GetSettings(Type type);
    }
}
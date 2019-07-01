using System;

namespace ExistAll.SimpleConfig.Extensions.GenericHost
{
    public interface IConfigProvider
    {
        object GetConfig(Type type);
    }
}
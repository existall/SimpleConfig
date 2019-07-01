using System;

namespace ExistAll.SimpleConfig
{
    public class ConfigOptionNonAttributeException : Exception
    {
        public ConfigOptionNonAttributeException(Type type) 
            : base(Resources.ConfigOptionAttributeTypeMessage(type))
        {
        }
    }
}
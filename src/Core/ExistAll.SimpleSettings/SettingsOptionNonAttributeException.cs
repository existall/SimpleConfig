using System;

namespace ExistAll.SimpleSettings
{
    public class SettingsOptionNonAttributeException : Exception
    {
        public SettingsOptionNonAttributeException(Type type) 
            : base(Resources.SettingsOptionAttributeTypeMessage(type))
        {
        }
    }
}
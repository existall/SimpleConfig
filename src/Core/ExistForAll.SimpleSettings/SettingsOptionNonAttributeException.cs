using System;

namespace ExistForAll.SimpleSettings
{
    public class SettingsOptionNonAttributeException : Exception
    {
        public SettingsOptionNonAttributeException(Type type) 
            : base(Resources.SettingsOptionAttributeTypeMessage(type))
        {
        }
    }
}
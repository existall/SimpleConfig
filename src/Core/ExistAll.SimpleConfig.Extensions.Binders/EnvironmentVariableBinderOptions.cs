using System;

namespace ExistAll.SimpleSettings.Binders
{
    public class EnvironmentVariableBinderOptions
    {
        public string Prefix { get; set; }

        public Func<string, string, string> VariableNameFormatter { get; set; }
    }
}
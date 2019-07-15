using System;

namespace ExistAll.SimpleConfig.Binders
{
    public class EnvironmentVariableBinderOptions
    {
        public string Prefix { get; set; }

        public Func<string, string, string> VariableNameFormatter { get; set; }
    }
}
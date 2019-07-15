using System;
using System.Text;

namespace ExistAll.SimpleConfig.Binders
{
    public class EnvironmentVariableBinder : ISectionBinder
    {
        public string Prefix { get; }

        public Func<string, string, string> VariableNameFormatter { get; set; }

        public EnvironmentVariableBinder(string prefix)
        {
            Prefix = prefix ?? throw new ArgumentNullException(nameof(prefix));
        }
		
        public EnvironmentVariableBinder() { }

        public void BindPropertyConfig(BindingContext context)
        {
            var sb = new StringBuilder();

            if (Prefix != null)
                sb.Append(Prefix);

            sb.Append(VariableNameFormatter != null
                ? VariableNameFormatter(context.Section, context.Key)
                : context.Key);

            var variableName = sb.ToString();

            var environmentVariable = Environment.GetEnvironmentVariable(variableName);
			
            context.SetNewValue(environmentVariable);
        }
    }
}
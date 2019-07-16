using ExistAll.SimpleConfig.Binder;

namespace ExistAll.SimpleConfig.Binders
{
    public static class CommandLineConfigBinderOptionsExtensions
    {
        public static CommandLineConfigBinderOptions AddPrefix(this CommandLineConfigBinderOptions options, char prefix)
        {
            options.AddArgumentPrefix(prefix);

            return options;
        }

        public static CommandLineConfigBinderOptions AddNewDelimiter(this CommandLineConfigBinderOptions options,
            string delimiter)
        {
            options.AddDelimiter(delimiter);

            return options;
        }

        public static CommandLineConfigBinderOptions SetCaseSensitivity(this CommandLineConfigBinderOptions options,
            bool isCaseSensitive)
        {
            options.IsCaseSensitive = isCaseSensitive;

            return options;
        }

        public static CommandLineConfigBinderOptions SetNameFormatter(this CommandLineConfigBinderOptions options,
            NameFormatter nameFormatter)
        {
            options.NameFormatter = nameFormatter;

            return options;
        }
        
        public static CommandLineConfigBinderOptions ClearAllPrefixes(this CommandLineConfigBinderOptions options,
            NameFormatter nameFormatter)
        {
            options.ClearArgumentPrefixes();

            return options;
        }
        
        public static CommandLineConfigBinderOptions ClearAllDelimiters(this CommandLineConfigBinderOptions options)
        {
            options.ClearDelimiters();

            return options;
        }
    }
}
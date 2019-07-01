namespace ExistAll.SimpleConfig.Extensions.GenericHost
{
    public static class SettingsProviderExtensions
    {
        public static T GetConfig<T>(this IConfigProvider target)
        {
            return (T) target.GetConfig(typeof(T));
        }
    }
}
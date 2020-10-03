namespace ExistAll.SimpleSettings.Extensions.GenericHost
{
    public static class SettingsProviderExtensions
    {
        public static T GetSettings<T>(this ISettingsProvider target)
        {
            return (T) target.GetSettings(typeof(T));
        }
    }
}
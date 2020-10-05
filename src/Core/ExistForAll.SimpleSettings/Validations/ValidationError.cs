namespace ExistForAll.SimpleSettings.Validations
{
    public class ValidationError
    {
        public string SettingsName { get; }
        public string ErrorMessage { get; }

        public ValidationError(string settingsName, string errorMessage)
        {
            SettingsName = settingsName;
            ErrorMessage = errorMessage;
        }
    }
}
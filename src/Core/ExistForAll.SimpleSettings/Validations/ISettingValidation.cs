using System.Threading.Tasks;

namespace ExistForAll.SimpleSettings.Validations
{
    public interface ISettingValidation<T> : ISettingsValidator
    {
        Task<ValidationResult> Validate(ValidationContext<T> context);
    }
}
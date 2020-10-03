using System.Threading.Tasks;

namespace ExistAll.SimpleSettings.Validations
{
    public interface ISettingValidation<T> : ISettingsValidator
    {
        Task<ValidationResult> Validate(ValidationContext<T> context);
    }
}
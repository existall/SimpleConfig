using System.Threading.Tasks;

namespace ExistAll.SimpleSettings.Validations
{
    public interface ISettingsValidator
    {
        Task<ValidationResult> Validate(ValidationContext context);
    }
}
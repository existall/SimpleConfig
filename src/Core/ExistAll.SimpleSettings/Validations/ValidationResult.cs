using System;
using System.Collections.Generic;
using System.Linq;

namespace ExistAll.SimpleSettings.Validations
{
    public class ValidationResult
    {
        private readonly List<ValidationError> _errors = new List<ValidationError>();

        public IEnumerable<ValidationError> Errors => _errors;

        public bool IsValid => !Errors.Any();

        public void AddError(ValidationError error)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));
            _errors.Add(error);
        }

    }
}
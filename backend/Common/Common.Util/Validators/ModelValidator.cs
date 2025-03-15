using System.ComponentModel.DataAnnotations;

namespace Common.Util.Validators
{
    public static class ModelValidator
    {
        public static (bool IsValid, List<ValidationResult> Errors) Validate<T>(T model)
        {
            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

            return (isValid, validationResults);
        }
    }
}

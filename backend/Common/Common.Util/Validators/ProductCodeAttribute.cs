using System.ComponentModel.DataAnnotations;

namespace Common.Util.Validators
{
    public class ProductCodeAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null || value is not string || value.ToString().Length != 20)
                return false;
            return true;
        }
    }
}

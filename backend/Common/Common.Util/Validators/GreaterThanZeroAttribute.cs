using System.ComponentModel.DataAnnotations;

namespace Common.Util.Validators
{
    public class GreaterThanZeroAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return false;
            try
            {
                var number = Convert.ToDouble(value);
                return number > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Util.Validators
{
    public class MinValueAttribute : ValidationAttribute
    {
        public int IntegerMinValue { get; set; }
        public decimal DecimalMinValue { get; set; }

        public MinValueAttribute(int integerMinValue) => IntegerMinValue = integerMinValue;

        public MinValueAttribute(decimal decimalMinValue) => DecimalMinValue = decimalMinValue;

        public override bool IsValid(object? value)
        {
            if (value is int && (int)value < IntegerMinValue)
                return false;

            if (value is decimal && (decimal)value < DecimalMinValue)
                return false;

            return true;
        }
    }
}

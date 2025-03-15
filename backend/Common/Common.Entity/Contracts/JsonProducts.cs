using System.ComponentModel.DataAnnotations;
using Common.Util.Validators;

namespace Common.Entity.Contracts
{
    public class JsonProducts
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [GreaterThanZero]
        public decimal Price { get; set; }

        [Required]
        [ProductCode]
        public string Code { get; set; }
    }
}

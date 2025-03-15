using System.ComponentModel.DataAnnotations;

namespace Common.Entity.Contracts
{
    public class JsonProducts
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "123")]
        public double Price { get; set; }

        [Required]
        [Length(20, 20)]
        public string Code { get; set; }
    }
}

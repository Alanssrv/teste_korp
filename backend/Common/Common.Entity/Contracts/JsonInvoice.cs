using System.ComponentModel.DataAnnotations;

namespace Common.Entity.Contracts
{
    public class JsonInvoice
    {
        [Required]
        public List<JsonInvoiceProducts> ProductsInvoice { get; set; }
    }
}

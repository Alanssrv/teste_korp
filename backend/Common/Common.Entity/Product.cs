using System.Text.Json.Serialization;

namespace Common.Entity
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public decimal Price { get; set; }

        public int InventoryBalance { get; set; }

        [JsonIgnore]
        public List<InvoiceProduct> InvoiceProducts { get; set; } = new List<InvoiceProduct>();
    }
}

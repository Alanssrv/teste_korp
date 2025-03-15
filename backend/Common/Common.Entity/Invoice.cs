using System.Text.Json.Serialization;

namespace Common.Entity
{
    public class Invoice : BaseEntity
    {
        public InvoiceState State { get; set; }

        [JsonIgnore]
        public List<InvoiceProduct> InvoiceProducts { get; set; } = new List<InvoiceProduct>();
    }
}

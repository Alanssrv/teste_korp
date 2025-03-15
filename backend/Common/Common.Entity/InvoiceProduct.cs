using System.Text.Json.Serialization;

namespace Common.Entity
{
    public class InvoiceProduct : BaseEntity
    {
        [JsonIgnore]
        public long InvoiceId { get; set; }
        public Invoice Invoice { get; set; } = null!;

        [JsonIgnore]
        public long ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}

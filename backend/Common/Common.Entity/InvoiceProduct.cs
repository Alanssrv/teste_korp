namespace Common.Entity
{
    public class InvoiceProduct : BaseEntity
    {
        public long InvoiceId { get; set; }
        public Invoice Invoice { get; set; } = null!;

        public long ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}

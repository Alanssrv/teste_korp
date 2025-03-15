namespace Common.Entity
{
    public class Invoice : BaseEntity
    {
        public InvoiceState State { get; set; }

        public List<InvoiceProduct> InvoiceProducts { get; set; } = new List<InvoiceProduct>();
    }
}

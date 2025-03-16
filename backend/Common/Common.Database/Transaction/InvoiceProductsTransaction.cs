using Common.Database.Transaction.Base;
using Common.Entity;

namespace Common.Database.Transaction
{
    public class InvoiceProductsTransaction
    {
        private readonly DatabaseTransaction _databaseTransaction;

        public InvoiceProductsTransaction(DatabaseTransaction databaseTransaction)
        {
            _databaseTransaction = databaseTransaction;
        }

        public void Insert(InvoiceProduct invoiceProduct)
        {
            _databaseTransaction.Context.InvoiceProducts.Add(invoiceProduct);
            _databaseTransaction.Context.SaveChanges();
        }

        public void InsertMany(List<InvoiceProduct> invoiceProducts)
        {
            _databaseTransaction.Context.InvoiceProducts.AddRange(invoiceProducts);
            _databaseTransaction.Context.SaveChanges();
        }

        public List<InvoiceProduct> GetAllInvoiceProducts() {
            return _databaseTransaction.Context.InvoiceProducts.ToList();
        }

        public List<InvoiceProduct> GetInvoiceProductsByInvoiceId(int id)
        {
            return _databaseTransaction.Context.InvoiceProducts.Where(invoiceProduct => invoiceProduct.InvoiceId == id).ToList();
        }
    }
}

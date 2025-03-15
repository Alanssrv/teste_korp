using System.Data.Common;
using Common.Database.Transaction.Base;
using Common.Entity;

namespace Common.Database.Transaction
{
    public class InvoiceTransaction
    {
        private readonly DatabaseTransaction _databaseTransaction;

        public InvoiceTransaction(DatabaseTransaction DatabaseTransaction)
        {
            _databaseTransaction = DatabaseTransaction;
        }

        public void Insert(Invoice invoice)
        {
            _databaseTransaction.Context.Invoice.Add(invoice);
            _databaseTransaction.Context.SaveChanges();
        }
    }
}

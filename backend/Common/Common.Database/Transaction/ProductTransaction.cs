using Common.Database.Transaction.Base;
using Common.Entity;
using Microsoft.Data.SqlClient;

namespace Common.Database.Transaction
{
    public class ProductTransaction
    {
        private readonly DatabaseTransaction _databaseTransaction;

        public ProductTransaction(DatabaseTransaction DatabaseTransaction)
        {
            _databaseTransaction = DatabaseTransaction;
        }

        public void Insert(Product product)
        {
            _databaseTransaction.Context.Products.Add(product);
            _databaseTransaction.Context.SaveChanges();
        }

        public List<Product> GettAllProducts()
        {
            using var context = new AppDbContext();

            return context.Products.ToList();
        }

        public Product GetProductByCode(string code)
        {
            using var context = new AppDbContext();

            return context.Products.Where(p => p.Code == code).FirstOrDefault();
        }

        public Product GetProductById(long id)
        {
            using var context = new AppDbContext();

            return context.Products.Where(p => p.Id == id).FirstOrDefault();
        }
    }
}

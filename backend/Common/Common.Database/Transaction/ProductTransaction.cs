using Common.Entity;
using Microsoft.Data.SqlClient;

namespace Common.Database.Transaction
{
    public class ProductTransaction
    {
        public void Insert(Product product)
        {
            using var context = new AppDbContext();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Products.Add(product);
                context.SaveChanges();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }

        public List<Product> GettAllProducts()
        {
            using var context = new AppDbContext();

            return context.Products.ToList();
        }
    }
}

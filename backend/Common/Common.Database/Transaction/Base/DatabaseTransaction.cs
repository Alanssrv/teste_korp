using Microsoft.EntityFrameworkCore.Storage;

namespace Common.Database.Transaction.Base
{
    public class DatabaseTransaction : IDisposable
    {
        private readonly AppDbContext _context;
        private readonly IDbContextTransaction _transaction;
        public DatabaseTransaction()
        {
            _context = new AppDbContext();
            _transaction = _context.Database.BeginTransaction();
        }

        public AppDbContext Context => _context;

        public void Commit() => _transaction.Commit();

        public void Rollback() => _transaction.Rollback();

        public void Dispose()
        {
            _transaction.Dispose();
            _context.Dispose();
        }
    }
}

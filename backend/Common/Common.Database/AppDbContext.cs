using Common.Entity;
using Microsoft.EntityFrameworkCore;

namespace Common.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string? connectionString = Environment.GetEnvironmentVariable("SQLConnectionString");
            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("Environment connection string not provide");

            optionsBuilder.UseSqlServer("Server=localhost\\MSSQLSERVER01;Database=TestKorp;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property(p => p.Id);
        }
    }
}

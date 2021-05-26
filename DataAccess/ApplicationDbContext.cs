namespace DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) :base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Pot> Pots { get; set; }

        public DbSet<Transaction> Transactions { get; set; }
    }
}

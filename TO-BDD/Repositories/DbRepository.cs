using Microsoft.EntityFrameworkCore;
using TO_BDD.Models;

namespace TO_BDD.Repositories
{
    public class DbRepository : DbContext
    {
        public DbRepository(DbContextOptions<DbRepository> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Users { get; set; }
    }
}

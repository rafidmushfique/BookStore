using Microsoft.EntityFrameworkCore;

namespace BookStore.Models.Domain
{
    public class BSDatabaseContext:DbContext
    {
        public BSDatabaseContext(DbContextOptions<BSDatabaseContext> options): base(options)
        {

        }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<author> Author { get; set; }

        public DbSet<Publisher> Publisher { get; set; }
    }
}

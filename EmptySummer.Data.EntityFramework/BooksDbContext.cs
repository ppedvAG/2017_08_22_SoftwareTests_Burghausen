using EmptySummer.Core.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EmptySummer.Data.EntityFramework
{
    public class BooksDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public BooksDbContext(string nameOrConnectionString) 
            : base(nameOrConnectionString)
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Author>().Property(a => a.RowVersion).IsRowVersion();
            modelBuilder.Entity<Book>().Property(b => b.RowVersion).IsRowVersion();
        }
    }
}

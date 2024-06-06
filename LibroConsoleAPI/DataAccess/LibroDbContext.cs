using LibroConsoleAPI.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LibroConsoleAPI.DataAccess
{
    public class LibroDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; } = null!;


        public LibroDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public LibroDbContext(IConfigurationRoot configurationRoot)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}

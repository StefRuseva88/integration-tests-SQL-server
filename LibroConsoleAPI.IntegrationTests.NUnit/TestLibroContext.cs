using LibroConsoleAPI.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LibroConsoleAPI.IntegrationTests.NUnit
{
    public class TestLibroDbContext : LibroDbContext
    {
        public TestLibroDbContext()
            : base(new ConfigurationBuilder().AddInMemoryCollection().Build())
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("TestDatabase");
            }
        }
    }
}

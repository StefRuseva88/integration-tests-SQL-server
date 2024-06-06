using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LibroConsoleAPI.DataAccess
{
    public class LibroDbContextFactory : IDesignTimeDbContextFactory<LibroDbContext>
    {
        public LibroDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var builder = new DbContextOptionsBuilder<LibroDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new LibroDbContext(builder.Options);
        }
    }
}

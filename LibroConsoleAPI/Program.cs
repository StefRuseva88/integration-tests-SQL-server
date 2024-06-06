using LibroConsoleAPI.Business;
using LibroConsoleAPI.DataAccess;
using LibroConsoleAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LibroConsoleApplication
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var engine = new Engine();

                var contextFactory = new LibroDbContextFactory();

                using var context = contextFactory.CreateDbContext(args);

                await context.Database.MigrateAsync();

                var bookRepository = new BookRepository(context);
                var bookManager = new BookManager(bookRepository);


                await DatabaseSeeder.SeedDatabaseAsync(context, bookManager);

                await engine.Run(bookManager);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
            }            
        }   
    }
}

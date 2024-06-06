using LibroConsoleAPI.Business;
using LibroConsoleAPI.Data.Models;
using Newtonsoft.Json;

namespace LibroConsoleAPI.DataAccess
{
    public static class DatabaseSeeder
    {
        public static async Task SeedDatabaseAsync(LibroDbContext context, BookManager bookManager)
        {            
            if (!context.Books.Any())
            {
                string jsonFilePath = Path.Combine("Data", "Seed", "books.json");
                string jsonData= File.ReadAllText(jsonFilePath);

                var books = JsonConvert.DeserializeObject<List<Book>>(jsonData);

                if (books != null)
                {
                    foreach (var book in books)
                    {
                        if (!context.Books.Any(b => b.ISBN == book.ISBN))
                        {
                            var newBook = new Book
                            {
                                ISBN = book.ISBN,
                                Title = book.Title,
                                Author = book.Author,
                                YearPublished = book.YearPublished,
                                Genre = book.Genre,
                                Pages = book.Pages,
                                Price = book.Price
                            };
                            await bookManager.AddAsync(newBook);
                        }
                    }

                    await context.SaveChangesAsync();
                }
            }
        }
    }
}

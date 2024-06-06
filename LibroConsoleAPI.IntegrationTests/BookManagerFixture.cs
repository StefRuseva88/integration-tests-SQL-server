using LibroConsoleAPI.Business.Contracts;
using LibroConsoleAPI.Business;
using LibroConsoleAPI.IntegrationTests;
using LibroConsoleAPI.Repositories;

//public class BookManagerFixture : IDisposable
//{
//    public TestLibroDbContext DbContext { get; private set; }
//    public IBookManager BookManager { get; private set; }

//    public BookManagerFixture()
//    {
//        DbContext = new TestLibroDbContext();
//        var bookRepository = new BookRepository(DbContext);
//        BookManager = new BookManager(bookRepository);
//    }

//    public void Dispose()
//    {
//        // Clean up resources after tests
//        DbContext.Dispose();
//    }
//}

public class BookManagerFixture : IAsyncLifetime
{
    public TestLibroDbContext DbContext { get; private set; }
    public IBookManager BookManager { get; private set; }

    public async Task InitializeAsync()
    {
        // Set up resources before tests
        DbContext = new TestLibroDbContext();
        var bookRepository = new BookRepository(DbContext);
        BookManager = new BookManager(bookRepository);
    }

    public async Task DisposeAsync()
    {
        // Clean up resources after tests
        if (DbContext != null)
        {
            // Delete all records from the database
            DbContext.Books.RemoveRange(DbContext.Books);
            await DbContext.SaveChangesAsync();

            // Dispose of the DbContext
            DbContext.Dispose();
        }
    }
}


using LibroConsoleAPI.Data.Models;
using LibroConsoleAPI.DataAccess;
using LibroConsoleAPI.DataAccess.Contracts;
using Microsoft.EntityFrameworkCore;

namespace LibroConsoleAPI.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibroDbContext _context;

        public BookRepository(LibroDbContext context)
        {
            _context = context;
        }

        public async Task AddBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(string isbn)
        {
            var book  = await _context.Books.FirstOrDefaultAsync(b => b.ISBN == isbn);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            var books = await _context.Books.ToListAsync();
            return books;
        }

        public async Task<Book> GetBookByISBNAsync(string isbn)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.ISBN == isbn);
            return book;
        }

        public async Task<IEnumerable<Book>> SearchBooksByTitleAsync(string titleFragment)
        {
            var books = await _context.Books.ToListAsync();
            return books.Where(b => b.Title.IndexOf(titleFragment, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
    }
}

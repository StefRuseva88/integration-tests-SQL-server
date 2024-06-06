using LibroConsoleAPI.Business.Contracts;
using LibroConsoleAPI.Data.Models;
using LibroConsoleAPI.DataAccess.Contracts;
using System.ComponentModel.DataAnnotations;

namespace LibroConsoleAPI.Business
{
    public class BookManager : IBookManager
    {
        private readonly IBookRepository _bookRepository;

        public BookManager(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task AddAsync(Book book)
        {
            if (!IsValid(book))
            {
                throw new ValidationException("Book is invalid.");
            }
            await _bookRepository.AddBookAsync(book);
        }

        public Task DeleteAsync(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
            {
                throw new ArgumentException("ISBN cannot be empty.");
            }

            return _bookRepository.DeleteBookAsync(isbn);
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllBooksAsync();

            if (!books.Any())
            {
                throw new KeyNotFoundException("No books found.");
            }

            return books;
        }

        public async Task<IEnumerable<Book>> SearchByTitleAsync(string titleFragment)
        {
            if (string.IsNullOrWhiteSpace(titleFragment))
            {
                throw new ArgumentException("Title fragment cannot be empty.");
            }

            var books = await _bookRepository.SearchBooksByTitleAsync(titleFragment);

            if (books == null || !books.Any())
            {
                throw new KeyNotFoundException("No books found with the given title fragment.");
            }

            return books;
        }

        public async Task<Book> GetSpecificAsync(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
            {
                throw new ArgumentException("ISBN cannot be empty.");
            }

            var book = await _bookRepository.GetBookByISBNAsync(isbn);

            if (book == null)
            {
                throw new KeyNotFoundException($"No book found with ISBN: {isbn}");
            }

            return book;
        }

        public async Task UpdateAsync(Book book)
        {
            if (!IsValid(book))
            {
                throw new ValidationException("Book is invalid.");
            }

            await _bookRepository.UpdateBookAsync(book);
        }

        private bool IsValid(Book book)
        {
            if (book == null)
            {
                return false;
            }

            var validateResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(book);

            if (!Validator.TryValidateObject(book, validationContext, validateResults, true))
            {
                foreach (var validationResult in validateResults)
                {
                    Console.WriteLine($"Validation Error: {validationResult.ErrorMessage}");
                }
                return false;
            }

            return true;
        }
    }
}

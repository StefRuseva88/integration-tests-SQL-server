using LibroConsoleAPI.Business.Contracts;
using LibroConsoleAPI.Data.Models;
using LibroConsoleAPI.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace LibroConsoleAPI.IntegrationTests
{
    //public class IntegrationTests : IClassFixture<BookManagerFixture>
    //{
    //    private readonly IBookManager _bookManager;
    //    private readonly TestLibroDbContext _dbContext;

    //    public IntegrationTests(BookManagerFixture fixture)
    //    {
    //        _bookManager = fixture.BookManager;
    //        _dbContext = fixture.DbContext;
    //    }

    public class IntegrationTests : IClassFixture<BookManagerFixture>, IAsyncLifetime
    {
        private readonly IBookManager _bookManager;
        private readonly TestLibroDbContext _dbContext;

        public IntegrationTests(BookManagerFixture fixture)
        {
            _bookManager = fixture.BookManager;
            _dbContext = fixture.DbContext;
        }

        // Implement IAsyncLifetime interface
        public async Task InitializeAsync()
        {
            // Additional setup code before each test (if needed)
        }

        public async Task DisposeAsync()
        {
            // Clean up resources after each test
            if (_dbContext != null)
            {
                // Delete all records from the database
                _dbContext.Books.RemoveRange(_dbContext.Books);
                await _dbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task AddBookAsync_ShouldAddBook()
        {
            // Arrange
            var newBook = new Book
            {
                Title = "Test Book",
                Author = "John Doe",
                ISBN = "1234567890123",
                YearPublished = 2021,
                Genre = "Fiction",
                Pages = 100,
                Price = 19.99
            };

            // Act
            await _bookManager.AddAsync(newBook);

            // Assert
            var bookInDb = await _dbContext.Books.FirstOrDefaultAsync(b => b.ISBN == newBook.ISBN);
            Assert.NotNull(bookInDb);
            Assert.Equal("Test Book", bookInDb.Title);
            Assert.Equal("John Doe", bookInDb.Author);
        }

        [Fact]
        public async Task AddBookAsync_TryToAddBookWithInvalidCredentials_ShouldThrowException()
        {
            // Arrange
            var newBook = new Book
            {
                Title = new string('A', 500),
                Author = "John Doe",
                ISBN = "1234567890123",
                YearPublished = 2021,
                Genre = "Fiction",
                Pages = 100,
                Price = 19.99
            };
            // Act
            var exception = Assert.ThrowsAsync<ValidationException>(() =>  _bookManager.AddAsync(newBook));
            // Assert
            Assert.Equal("Book is invalid.", exception.Result.Message);
        }

        [Fact]
        public async Task DeleteBookAsync_WithValidISBN_ShouldRemoveBookFromDb()
        {
            // Arrange
            var newBook = new Book
            {
                Title = "Test Book",
                Author = "John Doe",
                ISBN = "1234567890123",
                YearPublished = 2021,
                Genre = "Fiction",
                Pages = 100,
                Price = 19.99
            };
            await _bookManager.AddAsync(newBook);
            // Act
            await _bookManager.DeleteAsync(newBook.ISBN);
            // Assert
            var bookInDb = _dbContext.Books.FirstOrDefaultAsync();
            Assert.NotNull(bookInDb);
        }
        [Fact]
        public async Task DeleteBookAsync_TryToDeleteWithNullOrWhiteSpaceISBN_ShouldThrowException()
        {
            // Arrange
            var invalidISBN = string.Empty; 

            // Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _bookManager.DeleteAsync(invalidISBN));

            // Assert
            Assert.Equal("ISBN cannot be empty.", exception.Message);
        }

        [Fact]
        public async Task GetAllAsync_WhenBooksExist_ShouldReturnAllBooks()
        {
            // Arrange
            var newBook1 = new Book 
            { 
                Title = "Book1",
                Author = "Author1",
                ISBN = "1111111111116",
                YearPublished = 2022,
                Genre = "Fiction",
                Pages = 200,
                Price = 29.99 
            };
            var newBook2 = new Book 
            { 
                Title = "Book2",
                Author = "Author2",
                ISBN = "2222222222229", 
                YearPublished = 2021,
                Genre = "Non-Fiction",
                Pages = 150,
                Price = 24.99 
            };
            await _bookManager.AddAsync(newBook1);
            await _bookManager.AddAsync(newBook2);

            // Act
            var books = await _bookManager.GetAllAsync();

            // Assert
            Assert.NotNull(books);
            Assert.Equal(2, books.Count());
        }

        [Fact]
        public async Task GetAllAsync_WhenNoBooksExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange

            // Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _bookManager.GetAllAsync());

            // Assert
            Assert.Equal("No books found.", exception.Message);
        }

        [Fact]
        public async Task SearchByTitleAsync_WithValidTitleFragment_ShouldReturnMatchingBooks()
        {
            // Arrange
            var newBook1 = new Book
            {
                Title = "Book1",
                Author = "Author1",
                ISBN = "1111111111112",
                YearPublished = 2022,
                Genre = "Fiction",
                Pages = 200,
                Price = 29.99 
            };
            var newBook2 = new Book 
            { 
                Title = "Book2",
                Author = "Author2",
                ISBN = "2222222222223",
                YearPublished = 2021,
                Genre = "Non-Fiction",
                Pages = 150,
                Price = 24.99 
            };
            await _bookManager.AddAsync(newBook1);
            await _bookManager.AddAsync(newBook2);

            // Act
            var matchingBooks = await _bookManager.SearchByTitleAsync("Book");

            // Assert
            Assert.NotNull(matchingBooks);
            Assert.Equal(2, matchingBooks.Count());
        }

        [Fact]
        public async Task SearchByTitleAsync_WithInvalidTitleFragment_ShouldThrowKeyNotFoundException()
        {
            // Arrange

            // Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _bookManager.SearchByTitleAsync("InvalidTitleFragment"));

            // Assert
            Assert.Equal("No books found with the given title fragment.", exception.Message);
        }

        [Fact]
        public async Task GetSpecificAsync_WithValidIsbn_ShouldReturnBook()
        {
            // Arrange
            var newBook = new Book
            {
                Title = "Book1",
                Author = "Author1",
                ISBN = "1111111111115",
                YearPublished = 2022,
                Genre = "Fiction",
                Pages = 200,
                Price = 29.99 
            };
            await _bookManager.AddAsync(newBook);

            // Act
            var retrievedBook = await _bookManager.GetSpecificAsync(newBook.ISBN);

            // Assert
            Assert.NotNull(retrievedBook);
            Assert.Equal(newBook.ISBN, retrievedBook.ISBN);
        }

        [Fact]
        public async Task GetSpecificAsync_WithInvalidIsbn_ShouldThrowKeyNotFoundException()
        {
            // Arrange

            // Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _bookManager.GetSpecificAsync("InvalidISBN"));

            // Assert
            Assert.Equal("No book found with ISBN: InvalidISBN", exception.Message);
        }

        [Fact]
        public async Task UpdateAsync_WithValidBook_ShouldUpdateBook()
        {
            // Arrange
            var newBook = new Book
            {
                Title = "Book1",
                Author = "Author1",
                ISBN = "1111111111113",
                YearPublished = 2022,
                Genre = "Fiction",
                Pages = 200,
                Price = 29.99 
            };
            await _bookManager.AddAsync(newBook);

            // Updated book
            newBook.Title = "Updated Book";
            newBook.Price = 39.99;

            // Act
            await _bookManager.UpdateAsync(newBook);

            // Assert
            var updatedBook = await _bookManager.GetSpecificAsync(newBook.ISBN);
            Assert.Equal("Updated Book", updatedBook.Title);
            Assert.Equal(39.99, updatedBook.Price);
        }

        [Fact]
        public async Task UpdateAsync_WithInvalidBook_ShouldThrowValidationException()
        {
            // Arrange
            var invalidBook = new Book 
            { Title = "",
                Author = "Author1",
                ISBN = "1111111111114",
                YearPublished = 2022,
                Genre = "Fiction",
                Pages = 200,
                Price = 29.99 
            };

            // Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _bookManager.UpdateAsync(invalidBook));

            // Assert
            Assert.Equal("Book is invalid.", exception.Message);
        }

    }
}

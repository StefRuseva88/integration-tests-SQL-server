using LibroConsoleAPI.Business;
using LibroConsoleAPI.Business.Contracts;
using LibroConsoleAPI.Data.Models;
using LibroConsoleAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibroConsoleAPI.IntegrationTests.NUnit
{
    public  class IntegrationTests
    {
        private TestLibroDbContext dbContext;
        private IBookManager bookManager;

        [SetUp]
        public async Task SetUpAsync()
        {
            this.dbContext = new TestLibroDbContext();
            this.bookManager = new BookManager(new BookRepository(this.dbContext));
            await Task.CompletedTask;
        }

        [TearDown]
        public async Task TearDownAsync()
        {
            await this.dbContext.DisposeAsync();
        }

        [Test]
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
            await bookManager.AddAsync(newBook);

            // Assert
            var bookInDb = await dbContext.Books.FirstOrDefaultAsync(b => b.ISBN == newBook.ISBN);
            Assert.NotNull(bookInDb);
            Assert.That(bookInDb.Title, Is.EqualTo("Test Book"));
            Assert.That(bookInDb.Author, Is.EqualTo("John Doe"));
        }

        [Test]
        public async Task AddBookAsync_TryToAddBookWithInvalidCredentials_ShouldThrowException()
        {
            // Arrange
            var newBook = new Book
            {
                // Invalid Book: No Title
                Author = "John Doe",
                ISBN = "1234567890123",
                YearPublished = 2021,
                Genre = "Fiction",
                Pages = 100,
                Price = 19.99
            };

            // Act & Assert
            var ex = Assert.ThrowsAsync<ValidationException>(async () => await bookManager.AddAsync(newBook));
            Assert.That(ex.Message, Is.EqualTo("Book is invalid."));

        }

        [Test]
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
            await bookManager.AddAsync(newBook);
            // Act
            await bookManager.DeleteAsync(newBook.ISBN);
            // Assert
            var bookInDb = dbContext.Books.FirstOrDefaultAsync();
            Assert.NotNull(bookInDb);
        }

        [Test]
        public async Task DeleteBookAsync_TryToDeleteWithNullOrWhiteSpaceISBN_ShouldThrowException()
        {
            // Arrange
            var invalidISBN = string.Empty;

            // Act
            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await bookManager.DeleteAsync(invalidISBN));

            // Assert
            Assert.That(exception.Message, Is.EqualTo("ISBN cannot be empty."));
        }

        [Test]
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
            await bookManager.AddAsync(newBook1);
            await bookManager.AddAsync(newBook2);

            // Act
            var books = await bookManager.GetAllAsync();

            // Assert
            Assert.NotNull(books);
            Assert.That(books.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetAllAsync_WhenNoBooksExist_ShouldThrowKeyNotFoundException()
        {// Arrange

            // Act
            var exception =  Assert.ThrowsAsync<KeyNotFoundException>(async () => await bookManager.GetAllAsync());

            // Assert
            Assert.That(exception.Message, Is.EqualTo("No books found."));
        }

        [Test]
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
            await bookManager.AddAsync(newBook1);
            await bookManager.AddAsync(newBook2);

            // Act
            var matchingBooks = await bookManager.SearchByTitleAsync("Book");

            // Assert
            Assert.NotNull(matchingBooks);
            Assert.That(matchingBooks.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task SearchByTitleAsync_WithInvalidTitleFragment_ShouldThrowKeyNotFoundException()
        {
            // Arrange

            // Act
            var exception =  Assert.ThrowsAsync<KeyNotFoundException>(async () => await bookManager.SearchByTitleAsync("InvalidTitleFragment"));

            // Assert
            Assert.That(exception.Message, Is.EqualTo("No books found with the given title fragment."));
        }

        [Test]
        public async Task GetSpecificAsync_WithValidIsbn_ShouldReturnBook()
        {  // Arrange
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
            await bookManager.AddAsync(newBook);

            // Act
            var retrievedBook = await bookManager.GetSpecificAsync(newBook.ISBN);

            // Assert
            Assert.NotNull(retrievedBook);
            Assert.That(retrievedBook.ISBN, Is.EqualTo(newBook.ISBN));
        }

        [Test]
        public async Task GetSpecificAsync_WithInvalidIsbn_ShouldThrowKeyNotFoundException()
        {
            // Arrange

            // Act
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () => await bookManager.GetSpecificAsync("InvalidISBN"));

            // Assert
            Assert.That(exception.Message, Is.EqualTo("No book found with ISBN: InvalidISBN"));
        }

        [Test]
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
            await bookManager.AddAsync(newBook);

            // Updated book
            newBook.Title = "Updated Book";
            newBook.Price = 39.99;

            // Act
            await bookManager.UpdateAsync(newBook);

            // Assert
            var updatedBook = await bookManager.GetSpecificAsync(newBook.ISBN);
            Assert.That(updatedBook.Title, Is.EqualTo("Updated Book"));
            Assert.That(updatedBook.Price, Is.EqualTo(39.99));
        }

        [Test]
        public async Task UpdateAsync_WithInvalidBook_ShouldThrowValidationException()
        {
            // Arrange
            var invalidBook = new Book
            {
                Title = "",
                Author = "Author1",
                ISBN = "1111111111114",
                YearPublished = 2022,
                Genre = "Fiction",
                Pages = 200,
                Price = 29.99
            };

            // Act
            var exception =  Assert.ThrowsAsync<ValidationException>(async () => await bookManager.UpdateAsync(invalidBook));

            // Assert
            Assert.That(exception.Message, Is.EqualTo("Book is invalid."));
        }
    }
}

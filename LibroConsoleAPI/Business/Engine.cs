using LibroConsoleAPI.Business.Contracts;
using LibroConsoleAPI.Data.Models;

namespace LibroConsoleAPI.Business
{
    public class Engine : IEngine
    {
        public async Task Run(IBookManager bookManager)
        {
            bool exitRequested = false;
            while (!exitRequested)
            {
                Console.WriteLine($"{Environment.NewLine}Choose an option:");
                Console.WriteLine("1: Add Book");
                Console.WriteLine("2: Delete Book");
                Console.WriteLine("3: List All Books");
                Console.WriteLine("4: Update Book");
                Console.WriteLine("5: Find Book by Title");
                Console.WriteLine("X: Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await AddBook(bookManager);
                        break;
                    case "2":
                        await DeleteBook(bookManager);
                        break;
                    case "3":
                        await ListAllBooks(bookManager);
                        break;
                    case "4":
                        await UpdateBook(bookManager);
                        break;
                    case "5":
                        await FindBookByTitle(bookManager);
                        break;
                    case "X":
                    case "x":
                        exitRequested = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }

                static async Task AddBook(IBookManager bookManager)
                {
                    Console.WriteLine("Adding a new book:");

                    Console.Write("Enter ISBN: ");
                    var isbn = Console.ReadLine();

                    Console.Write("Enter Title: ");
                    var title = Console.ReadLine();

                    Console.Write("Enter Author: ");
                    var author = Console.ReadLine();

                    Console.Write("Enter Year Published (e.g., 2021): ");
                    if (!int.TryParse(Console.ReadLine(), out int yearPublished))
                    {
                        Console.WriteLine("Invalid year format.");
                        return;
                    }

                    Console.Write("Enter Genre: ");
                    var genre = Console.ReadLine();

                    Console.Write("Enter Number of Pages: ");
                    if (!int.TryParse(Console.ReadLine(), out int pages))
                    {
                        Console.WriteLine("Invalid pages format.");
                        return;
                    }

                    Console.Write("Enter Price: ");
                    if (!double.TryParse(Console.ReadLine(), out double price))
                    {
                        Console.WriteLine("Invalid price format.");
                        return;
                    }

                    var newBook = new Book
                    {
                        ISBN = isbn,
                        Title = title,
                        Author = author,
                        YearPublished = yearPublished,
                        Genre = genre,
                        Pages = pages,
                        Price = price
                    };

                    await bookManager.AddAsync(newBook);
                    Console.WriteLine("Book added successfully.");
                }

                static async Task DeleteBook(IBookManager bookManager)
                {
                    Console.Write("Enter ISBN of the book to delete: ");
                    string isbn = Console.ReadLine();
                    await bookManager.DeleteAsync(isbn);
                    Console.WriteLine("Book deleted successfully.");
                }

                static async Task ListAllBooks(IBookManager bookManager)
                {
                    var books = await bookManager.GetAllAsync();
                    if (books.Any())
                    {
                        foreach (var book in books)
                        {
                            Console.WriteLine($"ISBN: {book.ISBN}, Title: {book.Title}, Author: {book.Author}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No books available.");
                    }
                }

                static async Task UpdateBook(IBookManager bookManager)
                {
                    Console.Write("Enter ISBN of the book to update: ");
                    string isbn = Console.ReadLine();
                    var bookToUpdate = await bookManager.GetSpecificAsync(isbn);
                    if (bookToUpdate == null)
                    {
                        Console.WriteLine("Book not found.");
                        return;
                    }

                    Console.Write("Enter new Title (leave blank to keep current): ");
                    var title = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(title))
                    {
                        bookToUpdate.Title = title;
                    }

                    Console.Write("Enter new Author (leave blank to keep current): ");
                    var author = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(author))
                    {
                        bookToUpdate.Author = author;
                    }

                    Console.Write("Enter new Year Published value (leave blank to keep current): ");
                    var year = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(year))
                    {
                        bookToUpdate.YearPublished = int.Parse(year);
                    }

                    Console.Write("Enter new Genre (leave blank to keep current): ");
                    var genre = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(genre))
                    {
                        bookToUpdate.Genre = genre;
                    }

                    Console.Write("Enter new Pages value (leave blank to keep current): ");
                    var pages = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(pages))
                    {
                        bookToUpdate.Pages = int.Parse(pages);
                    }

                    Console.Write("Enter new Price of book (leave blank to keep current): ");
                    var price = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(price))
                    {
                        bookToUpdate.Price = double.Parse(price);
                    }

                    await bookManager.UpdateAsync(bookToUpdate);
                    Console.WriteLine("Book updated successfully.");
                }

                static async Task FindBookByTitle(IBookManager bookManager)
                {
                    Console.Write("Enter title or part of the title: ");
                    string titleFragment = Console.ReadLine();
                    var books = await bookManager.SearchByTitleAsync(titleFragment);

                    if (books.Any())
                    {
                        foreach (var book in books)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Year Published: {book.YearPublished}");
                            Console.WriteLine($"--Price: {book.Price}, Genre: {book.Genre}, Pages Count: {book.Pages}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No books found with the given title fragment.");
                    }
                }
            }
        }
    }
}

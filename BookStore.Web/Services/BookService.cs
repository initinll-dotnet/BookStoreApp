using BookStore.Web.Entities;
using BookStore.Web.Models;
using BookStore.Web.Repositories;

namespace BookStore.Web.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<BookModel>> GetBooks()
    {
        var books = await _bookRepository.GetBooksAsync();

        var booksModel = books
            .Select(book => new BookModel
            {
                Id = book.Id,
                Name = book.Name,
                AuthorName = book.AuthorName,
                ISBN = book.ISBN,
                Description = book.Description,
                Price = book.Price
            }).ToList();

        return booksModel;
    }

    public async Task<BookModel> GetBookByIdAsync(string bookId)
    {
        var book = await _bookRepository.GetBookByIdAsync(bookId);

        if (book == null)
        {
            return null;
        }

        var bookModel = new BookModel
        {
            Id = book.Id,
            Name = book.Name,
            AuthorName = book.AuthorName,
            ISBN = book.ISBN,
            Description = book.Description,
            Price = book.Price
        };

        return bookModel;
    }

    public async Task CreateBookAsync(BookModel model)
    {
        var book = new Book
        {
            Id = model.Id,
            Name = model.Name,
            AuthorName = model.AuthorName,
            ISBN = model.ISBN,
            Description = model.Description,
            Price = model.Price
        };

        await _bookRepository.CreateBookAsync(book);
    }

    public async Task UpdateBookAsync(string id, BookModel model)
    {
        var book = new Book
        {
            Id = id,
            Name = model.Name,
            AuthorName = model.AuthorName,
            ISBN = model.ISBN,
            Description = model.Description,
            Price = model.Price
        };

        await _bookRepository.UpdateBookAsync(book);
    }

    public async Task DeleteBookAsync(string id)
    {
        await _bookRepository.DeleteBookAsync(id);
    }
}

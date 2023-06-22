using BookStore.Web.Entities;

namespace BookStore.Web.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetBooksAsync();
    Task<Book> GetBookByIdAsync(string bookId);
    Task CreateBookAsync(Book book);
    Task UpdateBookAsync(Book book);
    Task DeleteBookAsync(string id);
}

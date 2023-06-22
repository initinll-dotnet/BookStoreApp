using BookStore.Web.Models;

namespace BookStore.Web.Services;

public interface IBookService
{
    Task<IEnumerable<BookModel>> GetBooks();
    Task<BookModel> GetBookByIdAsync(string bookId);
    Task CreateBookAsync(BookModel model);
    Task UpdateBookAsync(string id, BookModel model);
    Task DeleteBookAsync(string id);
}

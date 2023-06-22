using BookStore.Web.DBContext;
using BookStore.Web.Entities;
using BookStore.Web.Settings;

using MongoDB.Driver;

namespace BookStore.Web.Repositories;

public class BookRepository : IBookRepository
{
    private readonly IMongoCollection<Book> _bookCollection;

    public BookRepository(MongoContext dbContext)
    {
        _bookCollection = dbContext.Database.GetCollection<Book>(MongoCollectionNames.Books);
    }

    public async Task<IEnumerable<Book>> GetBooksAsync()
    {
        var filter = Builders<Book>.Filter.Empty;

        var books = (await _bookCollection.FindAsync(filter)).ToList();

        return books;
    }

    public async Task<Book> GetBookByIdAsync(string bookId)
    {
        var filter = Builders<Book>.Filter.Eq(book => book.Id, bookId);

        var book = await (await _bookCollection.FindAsync(filter)).FirstOrDefaultAsync();

        return book;
    }

    public async Task CreateBookAsync(Book book)
    {
        await _bookCollection.InsertOneAsync(book);
    }

    public async Task UpdateBookAsync(Book book)
    {
        var filter = Builders<Book>.Filter.Eq(book => book.Id, book.Id);

        var update = Builders<Book>.Update
            .Set(book => book.Name, book.Name)
            .Set(book => book.AuthorName, book.AuthorName)
            .Set(book => book.ISBN, book.ISBN)
            .Set(book => book.Description, book.Description)
            .Set(book => book.Price, book.Price);

        var updateResult = await _bookCollection.UpdateOneAsync(filter, update);
    }

    public async Task DeleteBookAsync(string id)
    {
        var filter = Builders<Book>.Filter.Eq(book => book.Id, id);

        var deletedResult = await _bookCollection.DeleteOneAsync(filter);
    }
}
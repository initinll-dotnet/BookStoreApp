using BookStore.Web.DBContext;
using BookStore.Web.Entities;

using MongoDB.Bson;
using MongoDB.Driver;

namespace BookStore.Web.Settings;

public class MongoDbIndexConfigurator : IMongoDbIndexConfigurator
{
    private readonly IMongoCollection<Book> _bookCollection;

    public MongoDbIndexConfigurator(MongoContext dbContext)
    {
        _bookCollection = dbContext.Database.GetCollection<Book>(MongoCollectionNames.Books);
    }

    public async Task ConfigureIndexesAsync()
    {
        // Check if indexes already exist
        var existingIndexes = _bookCollection.Indexes.List().ToList();

        // Create indexes only if they don't exist
        if (!existingIndexes.Any(i => IsDesiredIndex(i)))
        {
            var indexKeys = Builders<Book>
                .IndexKeys
                .Ascending(x => x.Name)
                .Ascending(x => x.AuthorName);

            var indexModel = new CreateIndexModel<Book>(indexKeys);

            await _bookCollection.Indexes.CreateOneAsync(indexModel);
        }
    }

    private bool IsDesiredIndex(BsonDocument index)
    {
        // Implement your logic to check if the index matches your desired index configuration
        // You can compare index keys, options, or any other properties to determine if it's the desired index
        // Return true if the index matches your desired configuration
        return index.Contains("Name") && index.Contains("AuthorName");
    }
}

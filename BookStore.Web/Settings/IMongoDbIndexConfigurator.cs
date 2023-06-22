namespace BookStore.Web.Settings;

public interface IMongoDbIndexConfigurator
{
    Task ConfigureIndexesAsync();
}

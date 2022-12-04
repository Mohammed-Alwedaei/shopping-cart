using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Products.API.Models;

namespace Products.API.Services;

public class ProductsService
{
    private readonly IMongoCollection<ProductModel> _productsCollection;

    public ProductsService(IOptions<ProductsStoreDatabaseConfig> productStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            productStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            productStoreDatabaseSettings.Value.DatabaseName);

        _productsCollection = mongoDatabase.GetCollection<ProductModel>(
            productStoreDatabaseSettings.Value.CollectionName);
    }

    public async Task<List<ProductModel>> GetProductsAsync() =>
        await _productsCollection.Find(_ => true).ToListAsync();

    public async Task<ProductModel?> GetProductAsync(string id) =>
        await _productsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateProductAsync(ProductModel newProduct) =>
        await _productsCollection.InsertOneAsync(newProduct);

    public async Task RemoveProductAsync(string id) =>
        await _productsCollection.DeleteOneAsync(x => x.Id == id);
}
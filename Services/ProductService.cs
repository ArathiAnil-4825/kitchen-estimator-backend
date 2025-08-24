using KitchenEstimatorAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KitchenEstimatorAPI.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(IOptions<KitchenEstimatorDatabaseSettings> dbSettings)
        {
            var client = new MongoClient(dbSettings.Value.ConnectionString);
            var database = client.GetDatabase(dbSettings.Value.DatabaseName);
            _products = database.GetCollection<Product>(dbSettings.Value.ProductsCollection);
        }

        public async Task<List<Product>> GetAsync() =>
            await _products.Find(p => true).ToListAsync();

        public async Task<Product?> GetAsync(string id) =>
            await _products.Find(p => p.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Product product) =>
            await _products.InsertOneAsync(product);

        public async Task UpdateAsync(string id, Product product) =>
            await _products.ReplaceOneAsync(p => p.Id == id, product);

        public async Task RemoveAsync(string id) =>
            await _products.DeleteOneAsync(p => p.Id == id);
    }
}

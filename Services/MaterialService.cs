using KitchenEstimatorAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KitchenEstimatorAPI.Services
{
    public class MaterialService
    {
        private readonly IMongoCollection<Material> _materials;

        public MaterialService(IOptions<KitchenEstimatorDatabaseSettings> dbSettings)
        {
            var client = new MongoClient(dbSettings.Value.ConnectionString);
            var database = client.GetDatabase(dbSettings.Value.DatabaseName);
            _materials = database.GetCollection<Material>(dbSettings.Value.MaterialsCollection);
        }

        public async Task<List<Material>> GetAsync() =>
            await _materials.Find(m => true).ToListAsync();

        public async Task<Material?> GetAsync(string id) =>
            await _materials.Find(m => m.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Material material) =>
            await _materials.InsertOneAsync(material);

        public async Task UpdateAsync(string id, Material material) =>
            await _materials.ReplaceOneAsync(m => m.Id == id, material);

        public async Task RemoveAsync(string id) =>
            await _materials.DeleteOneAsync(m => m.Id == id);
    }
}

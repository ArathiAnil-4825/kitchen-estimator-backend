using KitchenEstimatorAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KitchenEstimatorAPI.Services
{
    public class LaborRateService
    {
        private readonly IMongoCollection<LaborRate> _laborRates;

        public LaborRateService(IOptions<KitchenEstimatorDatabaseSettings> dbSettings)
        {
            var client = new MongoClient(dbSettings.Value.ConnectionString);
            var database = client.GetDatabase(dbSettings.Value.DatabaseName);
            _laborRates = database.GetCollection<LaborRate>(dbSettings.Value.LaborRatesCollection);
        }

        public async Task<List<LaborRate>> GetAsync() =>
            await _laborRates.Find(l => true).ToListAsync();

        public async Task<LaborRate?> GetAsync(string id) =>
            await _laborRates.Find(l => l.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(LaborRate laborRate) =>
            await _laborRates.InsertOneAsync(laborRate);

        public async Task UpdateAsync(string id, LaborRate laborRate) =>
            await _laborRates.ReplaceOneAsync(l => l.Id == id, laborRate);

        public async Task RemoveAsync(string id) =>
            await _laborRates.DeleteOneAsync(l => l.Id == id);
    }
}

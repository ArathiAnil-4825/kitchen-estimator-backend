
using KitchenEstimatorAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KitchenEstimatorAPI.Services
{
    public class ApprovalService
    {
        private readonly IMongoCollection<Approval> _approvals;

        public ApprovalService(IOptions<KitchenEstimatorDatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _approvals = database.GetCollection<Approval>("Approvals");
        }

        public async Task<List<Approval>> GetAsync() =>
            await _approvals.Find(_ => true).ToListAsync();

        public async Task<Approval?> GetAsync(string id) =>
            await _approvals.Find(a => a.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Approval approval) =>
            await _approvals.InsertOneAsync(approval);

        public async Task UpdateAsync(string id, Approval approval) =>
            await _approvals.ReplaceOneAsync(a => a.Id == id, approval);

        public async Task RemoveAsync(string id) =>
            await _approvals.DeleteOneAsync(a => a.Id == id);
    }
}

//using KitchenEstimatorAPI.Models;
//using Microsoft.Extensions.Options;
//using MongoDB.Driver;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace KitchenEstimatorAPI.Services
//{
//    public class ApprovalService
//    {
//        private readonly IMongoCollection<Approval> _approvals;

//        public ApprovalService(IOptions<KitchenEstimatorDatabaseSettings> dbSettings)
//        {
//            var client = new MongoClient(dbSettings.Value.ConnectionString);
//            var database = client.GetDatabase(dbSettings.Value.DatabaseName);
//            _approvals = database.GetCollection<Approval>(dbSettings.Value.ApprovalsCollection);
//        }

//        public async Task<List<Approval>> GetAsync() =>
//            await _approvals.Find(a => true).ToListAsync();

//        public async Task<Approval?> GetAsync(string id) =>
//            await _approvals.Find(a => a.Id == id).FirstOrDefaultAsync();

//        public async Task CreateAsync(Approval approval) =>
//            await _approvals.InsertOneAsync(approval);

//        public async Task UpdateAsync(string id, Approval approval) =>
//            await _approvals.ReplaceOneAsync(a => a.Id == id, approval);

//        public async Task RemoveAsync(string id) =>
//            await _approvals.DeleteOneAsync(a => a.Id == id);
//    }
//}

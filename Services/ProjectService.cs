using KitchenEstimatorAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace KitchenEstimatorAPI.Services
{
    public class ProjectService
    {
        private readonly IMongoCollection<Project> _projects;

        public ProjectService(IOptions<KitchenEstimatorDatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _projects = database.GetCollection<Project>(settings.Value.ProjectsCollection);
        }

        public async Task<List<Project>> GetAsync() =>
            await _projects.Find(_ => true).ToListAsync();

        public async Task<Project?> GetAsync(string id) =>
            await _projects.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Project newProject) =>
            await _projects.InsertOneAsync(newProject);

        public async Task UpdateAsync(string id, Project updatedProject) =>
            await _projects.ReplaceOneAsync(x => x.Id == id, updatedProject);

        public async Task RemoveAsync(string id) =>
            await _projects.DeleteOneAsync(x => x.Id == id);
    }
}

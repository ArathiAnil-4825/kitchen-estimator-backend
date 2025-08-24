using KitchenEstimatorAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using BCrypt.Net;

namespace KitchenEstimatorAPI.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IOptions<KitchenEstimatorDatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _users = database.GetCollection<User>("Users");
        }

        public async Task<User?> GetByUsernameAsync(string username) =>
            await _users.Find(u => u.Username == username).FirstOrDefaultAsync();

        public async Task<User?> GetByEmailAsync(string email) =>
            await _users.Find(u => u.Email == email).FirstOrDefaultAsync();

        public async Task<User?> GetByRoleAsync(string role) =>
            await _users.Find(u => u.Role == role).FirstOrDefaultAsync();
        public async Task CreateAsync(User user) =>
            await _users.InsertOneAsync(user);

        public bool VerifyPassword(string hash, string password) =>
            BCrypt.Net.BCrypt.Verify(password, hash);

        public string HashPassword(string password) =>
            BCrypt.Net.BCrypt.HashPassword(password);
        
           
    }
}

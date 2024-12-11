using MongoDB.Driver;
using UserRegistration.Models;

namespace UserRegistration.Repositories
{
    public class UserRepository
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UserRepository(IMongoDatabase database)
        {
            _usersCollection = database.GetCollection<User>("Users");
        }

        public async Task<List<User>> GetAllAsync()
        {
            try
            {
                return await _usersCollection.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error in GetAllAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            try
            {
                return await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error in GetByIdAsync: {ex.Message}");
                throw;
            }
        }

        public async Task CreateAsync(User user)
        {
            try
            {
                await _usersCollection.InsertOneAsync(user);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error in CreateAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            try
            {
                return await _usersCollection.Find(x => x.Email == email).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error in GetByEmailAsync: {ex.Message}");
                throw;
            }
        }
    }
}

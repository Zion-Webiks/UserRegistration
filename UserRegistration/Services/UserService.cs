using UserRegistration.Helpers;
using UserRegistration.Models;
using UserRegistration.Repositories;

namespace UserRegistration.Services
{
    public class UserService
    {
        private readonly UserRepository _repository;

        public UserService(UserRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<User?> RegisterUserAsync(string name, string email, string password)
        {
            var existingUser = await _repository.GetByEmailAsync(email);

            if (existingUser != null)
            {
                throw new Exception("Email is already registered.");
            }

            var newUser = new User
            {
                Username = name,
                Email = email,
                Password = PasswordHelper.HashPassword(password)
            };

            await _repository.CreateAsync(newUser);
            return newUser;
        }

        public async Task<User?> AuthenticateUserAsync(string email, string password)
        {
            var user = await _repository.GetByEmailAsync(email);

            if (user == null || !PasswordHelper.VerifyPassword(password, user.Password))
            {
                throw new Exception("Invalid email or password.");
            }

            return user;
        }
    }
}

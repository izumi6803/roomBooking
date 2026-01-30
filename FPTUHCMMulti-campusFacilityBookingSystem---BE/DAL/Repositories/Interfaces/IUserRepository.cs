using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(string id);
        Task<User?> GetByIdWithRoleAsync(string id);
        Task<List<User>> GetAllAsync();
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailOrUsernameAsync(string emailOrUsername);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> UsernameExistsAsync(string username);
        Task<int> AddAsync(User entity);
        Task<int> CreateAsync(User entity);
        Task<int> UpdateAsync(User entity);
        Task<bool> RemoveAsync(User entity);
        Task<(List<User> items, int total)> GetFilteredAsync(
            string? name, string? email, string? roleId, string? status, int page, int limit);
    }
}

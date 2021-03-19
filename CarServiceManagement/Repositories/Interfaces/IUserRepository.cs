using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarServiceManagement.Models;

namespace CarServiceManagement.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<int> UpdateUserInformation(User entity);
        Task<int> DeleteUserByIdAsync(int id);
        Task<int> AddUserAsync(User entity);
    }
}
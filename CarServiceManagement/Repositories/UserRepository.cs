using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarServiceManagement.Exceptions;
using CarServiceManagement.Models;
using CarServiceManagement.MySQL;
using CarServiceManagement.Repositories.Interfaces;

namespace CarServiceManagement.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IMySqlRepository _mySqlRepository;

        public UserRepository(IMySqlRepository mySqlRepository)
        {
            _mySqlRepository = mySqlRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            const string sql = "SELECT user_id, name, surname, position_id, role_id FROM users";
            var res = await _mySqlRepository.SelectItemsAsync<User>(sql);
            return res;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var sql = @"SELECT user_id, name, surname, position_id, role_id FROM users WHERE user_id = @Id";
            var result = await _mySqlRepository.SelectSingleItemAsync<User>(sql, new {Id = id});
            if (result == null)
            {
                throw new ValueNotFoundException("Value is null");
            }
            return result;
        }

        public async Task<int> UpdateUserInformation(User entity)
        {
            var sql = "UPDATE users SET name = @Name, surname = @Surname,position_id= @PositionId, role_id = @RoleId WHERE user_id = @UserId";
            var result = await _mySqlRepository.ExecuteAsync(sql, entity);
            return result;
        }

        public async Task<int> DeleteUserByIdAsync(int id)
        {
            var sql = "DELETE FROM users WHERE user_id = @UserId";
            var result = await _mySqlRepository.ExecuteAsync(sql, new {UserId = id});
            return result;
        }

        public async Task<int> AddUserAsync(User entity)
        {
            var sql = @"Insert into users (name, surname, position_id, role_id) VALUES (@Name, @Surname, @PositionId, @RoleId)";
            var result = await _mySqlRepository.ExecuteAsync(sql, entity);
            return result;
        }
        
    }
}
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> GetUsersAsync();
        Task CreateUserAsync(UserModel user);
        Task<UserModel> GetLoginInfoAsync(string email, string password);
        Task<UserModel> GetUserByIdAsync(int UserID);
        Task DeleteUserAsync(int UserID);
        Task<UserModel> GetUserByEmailAsync(string email);


    }
}

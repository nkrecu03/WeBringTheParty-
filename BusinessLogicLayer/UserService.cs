using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            
        }
        public async Task<IEnumerable<UserModel>> GetUsersAsync()
        {
            return await userRepository.GetUsersAsync();
        }

        public async Task CreateUserAsync(UserModel user)
        {
            await userRepository.CreateUserAsync(user);
        }

        public async Task<UserModel> LoginAsync(string email, string password)
        {
            return await userRepository.GetLoginInfoAsync(email, password);
        }
    }
}

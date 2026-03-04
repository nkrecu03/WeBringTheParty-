using BCrypt.Net;
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
        private readonly ICartRepository cartRepository;
        public UserService(IUserRepository userRepository, ICartRepository cartRepository)
        {
            this.userRepository = userRepository;
            this.cartRepository = cartRepository;

        }
        public async Task<IEnumerable<UserModel>> GetUsersAsync()
        {
            return await userRepository.GetUsersAsync();
        }

        public async Task CreateUserAsync(UserModel user)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            await userRepository.CreateUserAsync(user);
            await cartRepository.CreateCartAsync(user.UserID);
        }

        public async Task<UserModel> LoginAsync(string email, string password)
        {
            var user = await userRepository.GetUserByEmailAsync(email);

            if (user != null)
            { 
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

                if (isPasswordValid) 
                {
                    return user;
                }
            }
            return null;
        }
        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            return await userRepository.GetUserByIdAsync(id);
        }

        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            return await userRepository.GetUserByEmailAsync(email);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            await userRepository.DeleteUserAsync(id);
            return true;
        }

        public async Task EditUserAsync(UserModel user)
        {
            await userRepository.EditUserAsync(user);
        }
    }
}

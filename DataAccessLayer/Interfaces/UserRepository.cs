using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext appDbContext;
        public UserRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
            
        }

        public async Task<IEnumerable<UserModel>> GetUsersAsync()
        {
            return await appDbContext.Users.ToListAsync();
        }
        public async Task CreateUserAsync(UserModel user)
        {
            await appDbContext.Users.AddAsync(user);
            await appDbContext.SaveChangesAsync();
        }

        public async Task<UserModel> GetLoginInfoAsync(string email, string password)
        {
            return await appDbContext.Users
                .FirstOrDefaultAsync(u => u.EmailAddress == email && u.PasswordHash == password);
        }

        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            return await appDbContext.Users.FindAsync(id);
        }

        public async Task<UserModel> GetUserByEmailAsync(string email) { 
            return await appDbContext.Users.FirstOrDefaultAsync(u => u.EmailAddress == email);
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await appDbContext.Users.FindAsync(id);
            if (user != null)
            {
                appDbContext.Users.Remove(user);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task EditUserAsync(UserModel user)
        {
            appDbContext.Users.Update(user);
            await appDbContext.SaveChangesAsync();
        }

    }
}

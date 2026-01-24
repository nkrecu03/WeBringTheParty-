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
        private readonly UserDbContext userDbContext;
        public UserRepository(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
            
        }

        public async Task<IEnumerable<UserModel>> GetUsersAsync()
        {
            return await userDbContext.Users.ToListAsync();
        }
        public async Task CreateUserAsync(UserModel user)
        {
            await userDbContext.Users.AddAsync(user);
            await userDbContext.SaveChangesAsync();
        }

        public async Task<UserModel> GetLoginInfoAsync(string email, string password)
        {
            return await userDbContext.Users
                .FirstOrDefaultAsync(u => u.EmailAddress == email && u.Password == password);
        }

    }
}

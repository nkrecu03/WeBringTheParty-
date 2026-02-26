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

    }
}

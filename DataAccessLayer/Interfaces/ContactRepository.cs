using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Interfaces
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;

        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContactMessageModel>> GetAllMessagesAsync()
        {
            return await _context.ContactMessages
                .OrderByDescending(m => m.SentAt)
                .ToListAsync();
        }

        public async Task<ContactMessageModel> GetMessageByIdAsync(int id)
        {
            return await _context.ContactMessages.FindAsync(id);
        }

        public async Task CreateMessageAsync(ContactMessageModel message)
        {
            _context.ContactMessages.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStatusAsync(int id, string status)
        {
            var message = await _context.ContactMessages.FindAsync(id);
            if (message != null)
            {
                message.Status = status;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteMessageAsync(int id)
        {
            var message = await _context.ContactMessages.FindAsync(id);
            if (message != null)
            {
                _context.ContactMessages.Remove(message);
                await _context.SaveChangesAsync();
            }
        }
    }
}
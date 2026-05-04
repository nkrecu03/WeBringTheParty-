using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces
{
    public interface IContactRepository
    {
        Task<IEnumerable<ContactMessageModel>> GetAllMessagesAsync();
        Task<ContactMessageModel> GetMessageByIdAsync(int id);
        Task CreateMessageAsync(ContactMessageModel message);
        Task UpdateStatusAsync(int id, string status);
        Task DeleteMessageAsync(int id);
    }
}
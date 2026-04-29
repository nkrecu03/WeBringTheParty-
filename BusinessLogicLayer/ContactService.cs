using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepo;

        public ContactService(IContactRepository contactRepo)
        {
            _contactRepo = contactRepo;
        }

        public async Task<IEnumerable<ContactMessageModel>> GetAllMessagesAsync()
            => await _contactRepo.GetAllMessagesAsync();

        public async Task<ContactMessageModel> GetMessageByIdAsync(int id)
            => await _contactRepo.GetMessageByIdAsync(id);

        public async Task CreateMessageAsync(ContactMessageModel message)
            => await _contactRepo.CreateMessageAsync(message);

        public async Task UpdateStatusAsync(int id, string status)
            => await _contactRepo.UpdateStatusAsync(id, status);

        public async Task DeleteMessageAsync(int id)
            => await _contactRepo.DeleteMessageAsync(id);
    }
}
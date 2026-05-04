using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IRentalService
    {
        Task<IEnumerable<RentalItemModel>> GetAllRentalItemsAsync();
        Task<RentalItemModel> GetRentalItemByIdAsync(int id);
        Task<IEnumerable<RentalBookingModel>> GetBookingsForItemAsync(int rentalItemId);
        Task<bool> BookRentalAsync(int rentalItemId, int userId, DateTime date, string timeSlot);
        Task<Dictionary<DateTime, List<string>>> GetAvailabilityCalendarAsync(int rentalItemId, int year, int month);

        
        Task CreateRentalItemAsync(RentalItemModel item);
        Task EditRentalItemAsync(RentalItemModel item);
        Task DeleteRentalItemAsync(int id);
        Task<IEnumerable<RentalItemModel>> GetMostRentedAsync(int count);
    }
}

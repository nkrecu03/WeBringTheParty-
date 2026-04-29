using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IRentalRepository
    {
        Task<IEnumerable<RentalItemModel>> GetAllRentalItemsAsync();
        Task<RentalItemModel> GetRentalItemByIdAsync(int id);
        Task<IEnumerable<RentalBookingModel>> GetBookingsForItemAsync(int rentalItemId);
        Task CreateBookingAsync(RentalBookingModel booking);
        Task<bool> IsSlotAvailableAsync(int rentalItemId, DateTime date, string timeSlot);
        Task<IEnumerable<RentalItemModel>> GetMostRentedAsync(int count);


        Task CreateRentalItemAsync(RentalItemModel item);
        Task EditRentalItemAsync(RentalItemModel item);
        Task DeleteRentalItemAsync(int id);
    }
}

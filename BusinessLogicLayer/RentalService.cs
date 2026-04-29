using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepo;

        public RentalService(IRentalRepository rentalRepo)
        {
            _rentalRepo = rentalRepo;
        }

        public async Task<IEnumerable<RentalItemModel>> GetAllRentalItemsAsync()
            => await _rentalRepo.GetAllRentalItemsAsync();

        public async Task<RentalItemModel> GetRentalItemByIdAsync(int id)
            => await _rentalRepo.GetRentalItemByIdAsync(id);

        public async Task<IEnumerable<RentalBookingModel>> GetBookingsForItemAsync(int rentalItemId)
            => await _rentalRepo.GetBookingsForItemAsync(rentalItemId);

        public async Task<bool> BookRentalAsync(int rentalItemId, int userId, DateTime date, string timeSlot)
        {
            bool available = await _rentalRepo.IsSlotAvailableAsync(rentalItemId, date, timeSlot);
            if (!available) return false;

            var booking = new RentalBookingModel
            {
                RentalItemID = rentalItemId,
                UserID = userId,
                RentalDate = date.Date,
                TimeSlot = timeSlot,
                Status = "Confirmed",
                BookedAt = DateTime.UtcNow
            };

            await _rentalRepo.CreateBookingAsync(booking);
            return true;
        }

        // Returns a dictionary of date => list of UNAVAILABLE time slots
        // so the view can color dates and slots accordingly
        public async Task<Dictionary<DateTime, List<string>>> GetAvailabilityCalendarAsync(int rentalItemId, int year, int month)
        {
            var bookings = await _rentalRepo.GetBookingsForItemAsync(rentalItemId);
            var result = new Dictionary<DateTime, List<string>>();

            foreach (var booking in bookings)
            {
                var d = booking.RentalDate.Date;
                if (d.Year == year && d.Month == month)
                {
                    if (!result.ContainsKey(d))
                        result[d] = new List<string>();
                    result[d].Add(booking.TimeSlot);
                }
            }

            return result;
        }

        public async Task CreateRentalItemAsync(RentalItemModel item)
            => await _rentalRepo.CreateRentalItemAsync(item);

        public async Task EditRentalItemAsync(RentalItemModel item)
            => await _rentalRepo.EditRentalItemAsync(item);

        public async Task DeleteRentalItemAsync(int id)
            => await _rentalRepo.DeleteRentalItemAsync(id);
        public async Task<IEnumerable<RentalItemModel>> GetMostRentedAsync(int count)
    => await _rentalRepo.GetMostRentedAsync(count);
    }
}

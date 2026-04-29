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
    public class RentalRepository : IRentalRepository
    {
        private readonly AppDbContext _context;

        public RentalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RentalItemModel>> GetAllRentalItemsAsync()
        {
            return await _context.RentalItems
                .Where(r => r.IsActive)
                .ToListAsync();
        }

        public async Task<RentalItemModel> GetRentalItemByIdAsync(int id)
        {
            return await _context.RentalItems.FindAsync(id);
        }

        public async Task<IEnumerable<RentalBookingModel>> GetBookingsForItemAsync(int rentalItemId)
        {
            return await _context.RentalBookings
                .Where(b => b.RentalItemID == rentalItemId && b.Status == "Confirmed")
                .ToListAsync();
        }

        public async Task CreateBookingAsync(RentalBookingModel booking)
        {
            _context.RentalBookings.Add(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsSlotAvailableAsync(int rentalItemId, DateTime date, string timeSlot)
        {
            var dateOnly = date.Date;

            var existingBookings = await _context.RentalBookings
                .Where(b => b.RentalItemID == rentalItemId
                         && b.RentalDate.Date == dateOnly
                         && b.Status == "Confirmed")
                .Select(b => b.TimeSlot)
                .ToListAsync();

            if (!existingBookings.Any()) return true;

            // Full Day slot string as stored in the database
            const string fullDay = "Full Day (8AM–5PM)";

            // Trying to book Full Day — block if ANY booking exists that day
            if (timeSlot == fullDay) return false;

            // Full Day already booked — block everything else
            if (existingBookings.Contains(fullDay)) return false;

            // Otherwise check if this exact slot is already taken
            return !existingBookings.Contains(timeSlot);
        }

        public async Task<IEnumerable<RentalItemModel>> GetMostRentedAsync(int count)
        {
            // Step 1 — get top rental item IDs by booking count
            var topIds = await _context.RentalBookings
                .Where(b => b.Status == "Confirmed")
                .GroupBy(b => b.RentalItemID)
                .OrderByDescending(g => g.Count())
                .Take(count)
                .Select(g => g.Key)
                .ToListAsync();

            // Step 2 — fetch the actual items
            var items = await _context.RentalItems
                .Where(r => topIds.Contains(r.RentalItemID) && r.IsActive)
                .ToListAsync();

            // Step 3 — return in ranked order, fall back to newest if no bookings yet
            if (!items.Any())
            {
                return await _context.RentalItems
                    .Where(r => r.IsActive)
                    .Take(count)
                    .ToListAsync();
            }

            return topIds
                .Select(id => items.FirstOrDefault(r => r.RentalItemID == id))
                .Where(r => r != null)
                .ToList();
        }

        public async Task CreateRentalItemAsync(RentalItemModel item)
        {
            _context.RentalItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task EditRentalItemAsync(RentalItemModel item)
        {
            _context.RentalItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRentalItemAsync(int id)
        {
            var item = await _context.RentalItems.FindAsync(id);
            if (item != null)
            {
                item.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }
    }

}

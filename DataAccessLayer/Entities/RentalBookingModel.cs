using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class RentalBookingModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingID { get; set; }

        public int RentalItemID { get; set; }

        [ForeignKey("RentalItemID")]
        public RentalItemModel RentalItem { get; set; }

        public int UserID { get; set; }

        [Required]
        public DateTime RentalDate { get; set; }

        [Required]
        public string TimeSlot { get; set; }

        public string Status { get; set; } = "Confirmed"; // Confirmed, Cancelled

        public DateTime BookedAt { get; set; } = DateTime.UtcNow;
    }
}

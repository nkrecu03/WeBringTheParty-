using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class ContactMessageModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public string Phone { get; set; }

        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }

        public string Status { get; set; } = "Unread"; // Unread, Read, Replied

        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class OrderModel
    {
        [Key]
        public int OrderId { get; set; } //
        [ForeignKey("User")]
        public int UserId { get; set; } //
        public DateTime Date { get; set; } //

        // Navigation properties
        public UserModel User { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class OrderItemModel
    {
        [Key]
        public int OrderItemId { get; set; } //
        [ForeignKey("Order")]
        public int OrderId { get; set; } //
        [ForeignKey("Product")]
        public int ProductId { get; set; } //
        public int Quantity { get; set; } //

        // Navigation properties
        public OrderModel Order { get; set; }
        public ProductModel Product { get; set; }
    }
}

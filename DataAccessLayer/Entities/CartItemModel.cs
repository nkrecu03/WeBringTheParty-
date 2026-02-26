using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class CartItemModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartItemID { get; set; } //pk
        [ForeignKey("Cart")]
        public int CartID { get; set; } //fk
        [ForeignKey("Product")]
        public int ProductID { get; set; } //fk
        public int Quantity { get; set; }

        //nav properties
        public CartModel Cart { get; set; }
        public ProductModel Product { get; set; }
    }
}

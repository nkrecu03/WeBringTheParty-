using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class CartModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartID { get; set; } //pk
        [ForeignKey("User")]
        public int UserID { get; set; } //fk


        //nav properties
        public UserModel User { get; set; }
        public List<CartItemModel> CartItems { get; set; }

    }
}

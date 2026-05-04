using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class RentalItemModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RentalItemID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        public string KeyFeatures { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal RentalPricePerDay { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DeliveryFee { get; set; }

        public string ImageUrl { get; set; }

        public bool IsActive { get; set; } = true;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; } //pk
        public string FirstName { get; set; }
        public string LastName{ get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public string Role {  get; set; }  // Customer or Admin
        public bool isActive { get; set; }

        //default for normal users
        public UserModel()
        {
            Role = "Customer";
            isActive = true;
        }

    }
}

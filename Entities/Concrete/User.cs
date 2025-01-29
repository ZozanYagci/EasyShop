using Core.Entities;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class User:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } //Şifre hashlenmiş hali
        public string PasswordSalt { get; set; } //Şifre salt bilgisi
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserStatus Status { get; set; } // Örnek: "Active", "Inactive", "Banned", "Suspended" vs.

        public virtual ICollection<Order> Orders { get; set; } 
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set;}
    }
}

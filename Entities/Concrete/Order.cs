using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Order:BaseEntity
    {

        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; }

       
        public string Status { get; set; }  // Örnek: "Pending", "Shipped", "Delivered", "Cancelled"
        public virtual User User { get; set; } 
        public virtual ICollection<OrderDetail> Detail { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        
    }
}

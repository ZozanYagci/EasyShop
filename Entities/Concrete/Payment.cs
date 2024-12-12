using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Payment:BaseEntity
    {
    
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } // Ödeme yöntemi(kredi kartı, PayPal)
        public int OrderId { get; set; }
      
     
        public string Status { get; set; }  // Örnek: "Pending", "Completed", "Failed", "Refunded"

        public virtual Order Order { get; set; } 

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class OperationClaim:BaseEntity
    {

        // yetki - rol
        public string Name { get; set; } // Örn: Admin, User, Product.Add(yetkiler)

        public ICollection<AuthUserOperationClaim> AuthUserOperationClaims { get; set; }    
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class AuthUser:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; } // byte[] çünkü veritabanında binary olarak tutulacak
        public byte[] PasswordSalt { get; set; }
        public bool Status { get; set; }

        public ICollection<AuthUserOperationClaim> AuthUserOperationClaims { get; set; }    
    }
}

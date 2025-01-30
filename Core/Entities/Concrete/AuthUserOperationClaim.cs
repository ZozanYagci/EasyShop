using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class AuthUserOperationClaim :BaseEntity
    {
        //Kullanıcı birden fazla role sahip olabilsin (Hem admin hem editör gibi)
        public int AuthUserId { get; set; }
        public int OperationClaimId { get; set; }

        public AuthUser AuthUser { get; set; }
        public OperationClaim OperationClaim { get; set; }
    }
}

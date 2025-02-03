using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Exceptions
{
    public class UserAlreadyExistsException:CustomExceptionBase
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;
        public UserAlreadyExistsException(string message) : base(message)
        {
                
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Exceptions
{
    public class UserNotFoundException:CustomExceptionBase
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
        public UserNotFoundException(string message): base(message)
        {
                
        }
    }
}

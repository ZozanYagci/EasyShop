using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Exceptions
{
    public class InvalidPasswordException:CustomExceptionBase
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
        public override string CustomMessage => "Geçersiz Şifre";
        public InvalidPasswordException(string message): base(message)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Exceptions
{
    public abstract class CustomExceptionBase:Exception
    {
        public abstract HttpStatusCode StatusCode { get; }

        protected CustomExceptionBase(string message) : base(message)
        {
        }
    }
}

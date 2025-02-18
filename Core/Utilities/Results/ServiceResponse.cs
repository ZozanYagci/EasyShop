using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    // Servislerden dönecek yanıtları standartlaştırmak için kullanılacak.
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public ServiceResponse(T data, bool success, string message)
        {
            Data = data;
            Success = success;
            Message = message;
        }

        public ServiceResponse()
        {

        }
    }
}

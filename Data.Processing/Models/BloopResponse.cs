using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Processing.Models
{
    public class BloopResponse<T> where T : class
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public T ResponseObject { get; private set; }

        public BloopResponse(bool success, string message, T responseObject)
        {
            Success = success;
            Message = message;
            ResponseObject = responseObject;
        }
    }
}

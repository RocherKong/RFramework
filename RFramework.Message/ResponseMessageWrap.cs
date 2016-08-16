using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFramework.Message
{
    public class ResponseMessageWrap<T> where T : class, new()
    {
        public ResponseMessageWrap()
        {
            Body = new T();
            IsSuccess = true;
            ErrorCode = "000000";
        }
        public bool IsSuccess { get; set; }

        public String Message { get; set; }

        public String ErrorCode { get; set; }

        public T Body { get; set; }
    }
}

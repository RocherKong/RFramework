using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFramework.RException
{
    public class RException : Exception
    {
        public RException()
        {
            ErrorCode = "00001";
        }
        public String ErrorCode { get; set; }

        public RException(String errorCode, String message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}

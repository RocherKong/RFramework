using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFramework.Message
{
    public class AuthRequest : RequestMessage
    {
        public long AppId { get; set; }

        public String AppSecret { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFramework.Message
{
    public abstract class RequestMessage
    {
        public RequestMessage()
        {
            Header = new Message.RequestHeader { };
        }

        public RequestHeader Header { get; set; }
    }

    public class RequestHeader
    {
        public String Channel { get; set; }

        public long Operator { get; set; }

        public String IP { get; set; }
    }
}

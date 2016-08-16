using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFramework.Message
{
    public class IdRequest : RequestMessage
    {
        public long Id { get; set; }
    }
}

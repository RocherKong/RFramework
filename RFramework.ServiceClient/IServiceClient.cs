using RFramework.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFramework.ServiceClient
{
    public interface IServiceClient
    {
        ResponseMessageWrap<TResponseBody> Execute<TRequest, TResponseBody>(string FullCode, TRequest reqMsg)
            where TResponseBody : class, new()
            where TRequest : RequestMessage;

        String Execute(string FullCode, string reqMsg);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RFramework.Message;
using System.Net.Http;
using Newtonsoft.Json;

namespace RFramework.ServiceClient
{
    public class ServiceClient : IServiceClient
    {
        private ServiceResolver serviceResolver = new ServiceResolver();

        private ServiceClientContext Context { get { return ServiceClientContext.Instance; } }

        public String ApiToken { get { return Context.ApiToken; } }
        public string Execute(string FullCode, string reqMsg)
        {
            using (HttpClient client = new HttpClient())
            {
                string serviceUrl = serviceResolver.Resolve(FullCode);
                serviceUrl = String.Format("{0}?Token={1}", serviceUrl, ApiToken);
                HttpContent reqContent = new StringContent(reqMsg, Encoding.UTF8, "application/json");
                HttpResponseMessage Resp = client.PostAsync(serviceUrl, reqContent).Result;
                string resultStr = Resp.Content.ReadAsStringAsync().Result;
                var respModel = JsonConvert.DeserializeObject<ResponseMessageWrap<object>>(resultStr);
                if (respModel.ErrorCode == Context.FailureTokenCode)
                {
                    Context.ResetToken();
                    resultStr = Execute(FullCode, reqMsg);
                }
                return resultStr;
            }
        }

        public ResponseMessageWrap<TResponseBody> Execute<TRequest, TResponseBody>(string FullCode, TRequest reqMsg)
            where TRequest : RequestMessage
            where TResponseBody : class, new()
        {
            using (HttpClient client = new HttpClient())
            {
                string serviceUrl = serviceResolver.Resolve(FullCode);
                serviceUrl = String.Format("{0}?Token={1}", serviceUrl, ApiToken);
                HttpResponseMessage resp = client.PostAsJsonAsync<TRequest>(serviceUrl, reqMsg).Result;
                var respModel = JsonConvert.DeserializeObject<ResponseMessageWrap<TResponseBody>>(resp.Content.ReadAsStringAsync().Result);
                if (respModel.ErrorCode == Context.FailureTokenCode)
                {
                    Context.ResetToken();
                    respModel = Execute<TRequest, TResponseBody>(FullCode, reqMsg);
                }
                return respModel;
            }
        }
    }
}

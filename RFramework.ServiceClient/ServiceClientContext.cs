using Newtonsoft.Json;
using RFramework.Message;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RFramework.ServiceClient
{
    /// <summary>
    /// ServiceClient 上下文
    /// </summary>
    public class ServiceClientContext
    {
        public String ApiToken { get; private set; }
        private static readonly object sysObject = new object();

        public ApiConfig Config { get; set; }

        public long AppId { get { return Config.AppId; } }

        public String AppSecret { get { return Config.AppSecret; } }

        public String ServiceAuth { get { return Config.ServiceAuth; } }
        /// <summary>
        /// Api 授权 -Token验证失败Code
        /// </summary>
        public String FailureTokenCode = "00004";
        private ServiceClientContext()
        {
            Init();
        }

        private void Init()
        {
            LoadApiConfig();
            ResetToken();
        }

        private void LoadApiConfig()
        {
            Config = ConfigurationManager.GetSection("ServiceClient") as ApiConfig;
        }
        private static ServiceClientContext instance;
        public static ServiceClientContext Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (sysObject)
                    {
                        if (instance == null)
                        {
                            instance = new ServiceClientContext();
                        }
                    }
                }
                return instance;
            }
        }

        public void ResetToken()
        {
            using (HttpClient client = new HttpClient())
            {
                AuthRequest reqMsg = new AuthRequest
                {
                    AppId = AppId,
                    AppSecret = AppSecret
                };
                //Install-Package Microsoft.AspNet.WebApi.Client 
                HttpResponseMessage resp = client.PostAsJsonAsync<AuthRequest>(ServiceAuth, reqMsg).Result;
                var respModel = JsonConvert.DeserializeObject<ResponseMessageWrap<AuthResponse>>(resp.Content.ReadAsStringAsync().Result);

                if (!respModel.IsSuccess)
                {
                    throw new Exception(respModel.ErrorCode);
                }
                ApiToken = respModel.Body.Token;

            }
        }

    }
}

using Newtonsoft.Json;
using RFramework.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RFramework.ServiceClient
{
    /// <summary>
    /// 服务解析器
    /// </summary>
    public class ServiceResolver
    {

        public ServiceClientContext Context { get { return ServiceClientContext.Instance; } }
        public ServiceResolver()
        {
            if (!isInitialized)
            {
                Init();
            }

        }

        private static bool isInitialized = false;

        public void Init()
        {
            LoadConfigService();
        }
        public ServiceConfigResponse ConfigInfo { get; private set; }
        public void LoadConfigService()
        {
            using (HttpClient client = new HttpClient())
            {
                string serviceApi = String.Format("{0}?Token={1}", Context.Config.ServiceConfigApi,Context.ApiToken);
                string respStr = client.GetStringAsync(serviceApi).Result;
                var resp = JsonConvert.DeserializeObject<ResponseMessageWrap<ServiceConfigResponse>>(respStr);
                if (resp.ErrorCode == Context.FailureTokenCode)
                {
                    Context.ResetToken();
                    LoadConfigService();
                }
                ConfigInfo = resp.Body;
                isInitialized = true;

            }
        }

        /// <summary>
        /// 通过FullCode 解析到服务API地址
        /// </summary>
        /// <param name="FullCode"></param>
        /// <returns></returns>
        public String Resolve(String FullCode)
        {
            List<ServiceConfigResponse.Service> services = ConfigInfo.Services as List<ServiceConfigResponse.Service>;
            string[] codes = FullCode.Split('.');
            if (codes.Length != 3)
            {
                throw new ArgumentException("参数【FullCode】格式不合法！");

            }
            string serviceCode = codes[0];
            string controllerCode = codes[1];
            string actionCode = codes[2];

            ServiceConfigResponse.Service serviceItem = services.FirstOrDefault(m => m.Code == serviceCode);
            ServiceConfigResponse.Controller controllerItem = serviceItem.Controllers.FirstOrDefault(m => m.Code == controllerCode);
            ServiceConfigResponse.Action actionItem = controllerItem.Actions.FirstOrDefault(m => m.Code == actionCode);
            return String.Format("{0}/{1}/{2}",serviceItem.Host,controllerItem.Name,actionItem.Name);

        }
    }
}

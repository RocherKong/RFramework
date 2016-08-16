using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RFramework.ServiceClient
{
    public class ApiConfig
    {
        public long AppId { get; set; }

        public String AppSecret { get; set; }

        public String ServiceAuth { get; set; }

        public String ServiceConfigApi { get; set; }
    }

    public class ServiceClientSectionHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            ApiConfig Config = new ApiConfig();
            XmlNode childNode = section.ChildNodes[0];
            Config.AppId = long.Parse(childNode.Attributes["AppId"].Value);
            Config.AppSecret = childNode.Attributes["AppSecret"].Value;
            Config.ServiceAuth = childNode.Attributes["ServiceAuth"].Value;
            Config.ServiceConfigApi = childNode.Attributes["ServiceConfigApi"].Value;
            return Config;
        }
    }
}

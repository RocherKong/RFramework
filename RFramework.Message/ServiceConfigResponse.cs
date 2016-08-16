using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RFramework.Message
{
    [XmlRoot(ElementName = "ServiceConfig", Namespace = "http://i4ta.com/schemas/ServiceConfig.xsd")]
    public class ServiceConfigResponse
    {
        public ServiceConfigResponse()
        {
            Services = new List<Service>();
        }

        public List<Service> Services { get; set; }

        public class Service
        {
            public List<Controller> Controllers { get; set; }
            public Service()
            {
                Controllers = new List<Controller>();
            }
            [XmlAttribute]
            public String Code { get; set; }

            [XmlAttribute]
            public String Name { get; set; }

            [XmlAttribute]
            public String Host { get; set; }

            [XmlAttribute]
            public String Description { get; set; }
        }

        public class Controller
        {
            public List<Action> Actions { get; set; }
            public Controller()
            {
                Actions = new List<Action>();
            }
            [XmlAttribute]
            public String Code { get; set; }
            [XmlAttribute]
            public String Name { get; set; }
        }

        public class Action
        {
            [XmlAttribute]
            public String Code { get; set; }
            [XmlAttribute]
            public String Name { get; set; }
            [XmlAttribute]
            public String Description { get; set; }

        }

    }
}

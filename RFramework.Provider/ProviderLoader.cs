using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RFramework.Provider
{
    public class ProviderLoader
    {
        private String ConfigPath = "Config/Provider.xml";

        public IList<Provider> Providers { get; private set; }

        protected String ConfigFullPath { get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigPath); } }

        protected ProviderLoader()
        {
            LoadConfig(ConfigFullPath);
            WatchConfig(ConfigFullPath);
        }


        private FileSystemWatcher fileWatcher;
        private void WatchConfig(String fullPath)
        {
            string directoryName = Path.GetDirectoryName(fullPath);
            string fileName = Path.GetFileName(fullPath);
            fileWatcher = new FileSystemWatcher(directoryName);
            fileWatcher.Filter = fileName;
            fileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            fileWatcher.Changed += fileWatcher_Changed;
            fileWatcher.EnableRaisingEvents = true;

        }

        private DateTime lastChangedTime = DateTime.Now;

        private int twoTimeInterval = 500;

        public Action<ProviderLoader> ConfigChanged { get; set; }

        private void fileWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            var timeInterval = (DateTime.Now - lastChangedTime).TotalMilliseconds;
            if (timeInterval < twoTimeInterval)
            {
                return;
            }

            LoadConfig(ConfigFullPath);
            ConfigChanged?.Invoke(Instance);
            lastChangedTime = DateTime.Now;


        }

        public Provider LoadProvider(String ProviderName)
        {
            return Providers.First(x => x.Name == ProviderName);
        }

        private static ProviderLoader instance;

        private static readonly object syscLock = new object();

        public static ProviderLoader Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syscLock)
                    {
                        if (instance == null)
                        {
                            instance = new ProviderLoader();
                        }
                    }
                }

                return instance;
            }
        }

        private void LoadConfig(string fullPath)
        {
            using (FileStream streamReader = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProviderConfig));
                ProviderConfig config = xmlSerializer.Deserialize(streamReader) as ProviderConfig;
                Providers = config.Providers;
            }
        }

        [XmlRoot(ElementName = "ProviderConfig", Namespace = "http://i4ta.com/schemas/ProviderConfig.xsd")]
        public class ProviderConfig
        {
            public List<Provider> Providers { get; set; }
        }

        public class Provider
        {
            [XmlAttribute]
            public String Name { get; set; }
            [XmlAttribute]
            public String Type { get; set; }

            public IList<Parameter> Parameters { get; set; }
            [XmlIgnore]
            public String TypeName { get { return Type.Split(';')[0]; } }
            [XmlIgnore]
            public String AssemblyString { get { return Type.Split(';')[1]; } }
        }

        public class Parameter
        {
            [XmlAttribute]
            public String Key { get; set; }
            [XmlAttribute]
            public String Value { get; set; }
        }


    }
}

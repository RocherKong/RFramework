using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RFramework.Provider
{
    public class BaseProvider : ProviderBase
    {
        static BaseProvider()
        {
            ProviderLoader.Instance.ConfigChanged = (provider) =>
            {
                ProviderContainer.Clear();
            };
        }

        private static readonly object sysnLock = new object();

        private static Hashtable ProviderContainer = new Hashtable();

        public static T LoadInstance<T>(String ProviderName) where T : ProviderBase
        {
            if (ProviderContainer[ProviderName] == null)
            {
                lock (sysnLock)
                {
                    if (ProviderContainer[ProviderName] == null)
                    {
                        var ProviderConfig = ProviderLoader.Instance.LoadProvider(ProviderName);
                        T provider = Assembly.Load(ProviderConfig.AssemblyString).CreateInstance(ProviderConfig.TypeName) as T;
                        NameValueCollection nvCol = new NameValueCollection();
                        foreach (var nv in ProviderConfig.Parameters)
                        {
                            nvCol.Add(nv.Key, nv.Value);
                        }

                        provider.Initialize(ProviderName, nvCol);
                        ProviderContainer.Add(ProviderName, provider);
                    }
                }
            }

            return ProviderContainer[ProviderName] as T;

        }
    }
}

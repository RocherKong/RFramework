using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFramework.Provider
{
    public abstract class ProviderBase
    {
        protected ProviderBase()
        {

        }

        public virtual void Initialize(string name, NameValueCollection config)
        {

        }

        public virtual string Description { get; }

        public virtual string Name { get; }
    }
}

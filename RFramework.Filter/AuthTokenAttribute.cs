using RFramework.Cache.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;

namespace RFramework.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthTokenAttribute : AuthorizationFilterAttribute
    {
        private CacheProvider cacheProvider = CacheProvider.LoadInstance<CacheProvider>("RedisCacheProvider");

        protected virtual void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {

            if (actionContext==null)
            {
                throw new ArgumentException("Filter Error:actionContext is null.");
            }
            throw new RException()
        }

    }
}

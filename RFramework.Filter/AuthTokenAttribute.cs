using RFramework.Cache.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Net.Http;

namespace RFramework.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthTokenAttribute : AuthorizationFilterAttribute
    {
        private CacheProvider cacheProvider = CacheProvider.LoadInstance<CacheProvider>("RedisCacheProvider");

        protected virtual void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {

            if (actionContext == null)
            {
                throw new ArgumentException("Filter Error:actionContext is null.");
            }
            throw new RFramework.RException.RException("00004", "Token 验证失效");
        }

        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            if (!actionContext.ActionDescriptor.GetCustomAttributes<NoneTokenAttribute>().Any<NoneTokenAttribute>())
            {
                return actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<NoneTokenAttribute>().Any<NoneTokenAttribute>();
            }

            return true;
        }

        protected virtual bool IsAuthorized(HttpActionContext actionContext)
        {
            KeyValuePair<String, String> obj_Token = actionContext.Request.GetQueryNameValuePairs().FirstOrDefault();
            if (String.IsNullOrEmpty(obj_Token.Key))
            {
                throw new RException.RException("00005", "Token 不能为空");
            }
            bool isSuccess = CheckToken(obj_Token.Value);
            return isSuccess;
        }

        private bool CheckToken(String TokenValue)
        {
            string strCacheKey = RFramework.Const.CacheKey.GetTokenKey(TokenValue);
            return cacheProvider.IsExist(strCacheKey);
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException("actionContext");
            }
            if (!SkipAuthorization(actionContext) && !IsAuthorized(actionContext))
            {
                HandleUnauthorizedRequest(actionContext);
            }
        }
    }
}

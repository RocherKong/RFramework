using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFramework.Const
{
    public class CacheKey
    {
        public const string App = "App:{0}";

        public const string Token = "Token:{0}";
        public static String GetApp(long AppId)
        {
            return String.Format(App, AppId);
        }

        public static String GetTokenKey(string token)
        {
            return String.Format(Token, token); 
        }
    }
}

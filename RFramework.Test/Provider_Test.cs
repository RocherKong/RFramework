using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RFramework.Cache.Interface;
using RFramework.Provider;

namespace RFramework.Test
{
    [TestClass]
    public class Provider_Test
    {
        //[TestMethod]
        //public void ProviderLoad_Test()
        //{
        //  var resp= BaseProvider.LoadInstance<CacheProvider>("RedisCacheProvider");
        //    Assert.IsTrue(true);
        //}

        //public CacheProvider CacheProvider
        //{
        //    get {
        //        return BaseProvider.LoadInstance<CacheProvider>("RedisCacheProvider");
        //    }
        //}

        //[TestMethod]
        //public void Add()
        //{
        //    String token = Guid.NewGuid().ToString("N");
        //    bool isSuccess = CacheProvider.Add<String>(String.Format("Token:{0}", token), "这里是用户会话数据");
        //    Assert.IsTrue(isSuccess);
        //}
    }
}

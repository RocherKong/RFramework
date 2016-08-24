using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RFramework.Cache.Interface;
using RFramework.Provider;

namespace RFramework.Test
{
    [TestClass]
    public class Provider_Test
    {
        [TestMethod]
        public void ProviderLoad_Test()
        {
          var resp= BaseProvider.LoadInstance<CacheProvider>("RedisCacheProvider");
            Assert.IsTrue(true);
        }
    }
}

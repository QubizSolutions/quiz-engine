using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace Qubiz.QuizEngine
{
    public class ApplicationMemoryCache : MemoryCache
    {
        private static ApplicationMemoryCache instance;

        private ApplicationMemoryCache() : base("ApplicationMemoryCache") { }

        public static ApplicationMemoryCache Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ApplicationMemoryCache();
                }
                return instance;
            }
        }
    }
}
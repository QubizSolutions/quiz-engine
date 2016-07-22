using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Repositories;
using System;
using System.Linq;
using System.Runtime.Caching;
using System.Web.Mvc;

namespace Qubiz.QuizEngine
{
    public static class Authorizer 
    {
        public static bool IsAdmin(string userName)
        {
            bool isAuthorized = false;

            ApplicationMemoryCache cache = ApplicationMemoryCache.Instance;
            string key = "GetAllAdmins()";
            Admin[] admins = cache[key] as Admin[];

            if (admins == null) { 
                IAdminRepository repository = DependencyResolver.Current.GetService<IAdminRepository>();
                admins = repository.GetAllAdmins();
                cache.Add(new CacheItem(key, admins), new CacheItemPolicy() { AbsoluteExpiration = DateTime.UtcNow.AddMinutes(60) });
            }

            isAuthorized = admins.Any(a => a.Name == userName);
            return isAuthorized;
        }
    }
}
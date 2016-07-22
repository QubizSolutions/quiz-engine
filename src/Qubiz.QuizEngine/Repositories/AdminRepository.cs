using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Linq;

namespace Qubiz.QuizEngine.Repositories
{
    public interface IAdminRepository
    {
        Admin[] GetAllAdmins();
        void UpdateAdmins(Admin[] admins);
    }

    public class AdminRepository : IAdminRepository
    {
		private readonly IRepository repository;

        public AdminRepository(IRepository repository)
        {
            this.repository = repository;
        }

        public Admin[] GetAllAdmins()
        {
            return repository.All<Admin>().ToArray();
        }

        public void UpdateAdmins(Admin[] admins)
        {
            ApplicationMemoryCache.Instance.Remove("GetAllAdmins()");
            Admin[] existingAdmins = repository.All<Admin>().ToArray();

            foreach (Admin admin in admins)
            {
                if (admin.ID == Guid.Empty)
                    admin.ID = Guid.NewGuid();

                repository.Upsert(admin);
            }

            foreach (Admin item in existingAdmins.Where(es => !admins.Any(ns => ns.ID == es.ID)))
            {
                repository.Delete(item);
            }

            repository.SaveChanges();
        }
    }
}
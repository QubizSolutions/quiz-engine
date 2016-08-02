using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Services.AdminService
{
    public interface IAdminService
    {
        void AddAdmin(Admin admin);
        bool DeleteAdmin(Guid id);
        void UpdateAdmin(Admin admin);
        Task<Admin[]> GetAllAdmins();
        Task<Admin> GetAdmin(Guid id);
    }
}

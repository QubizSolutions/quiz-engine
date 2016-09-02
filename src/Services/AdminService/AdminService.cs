using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Database;
using Qubiz.QuizEngine.Database.Repositories;
using Qubiz.QuizEngine.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWorkFactory unitOfWorkFactory;

        public AdminService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<ValidationError[]> AddAdminAsync(Admin admin, string originator)
        {
            using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
            {
                if (!admin.Name.ToLowerInvariant().Contains(@"qubiz\"))
                    admin.Name = @"QUBIZ\" + admin.Name;

                Admin adminUser = await unitOfWork.AdminRepository.GetByNameAsync(admin.Name);
                if (adminUser != null)
                    return new ValidationError[1] { new ValidationError() { Message = "Name already exists!" } };

                admin.Name = admin.Name.Substring(0, 6).ToUpper() + admin.Name.Substring(6);

                unitOfWork.AdminRepository.Create(admin);

                await unitOfWork.SaveAsync();

                return new ValidationError[0];
            }
        }

        public async Task<ValidationError[]> DeleteAdminAsync(Guid id, string originator)
        {
            using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
            {
                Admin admin = await unitOfWork.AdminRepository.GetByIDAsync(id);

                if (admin.Name == originator)
                    return new ValidationError[1] { new ValidationError() { Message = "Can't delete yourself" } };

                unitOfWork.AdminRepository.Delete(admin);

                await unitOfWork.SaveAsync();

                return new ValidationError[0];
            }
        }

        public async Task<Admin> GetAdminAsync(Guid id)
        {
            using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
            {
                return await unitOfWork.AdminRepository.GetByIDAsync(id);
            }
        }

        public async Task<Admin[]> GetAllAdminsAsync()
        {
            using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
            {
                return await unitOfWork.AdminRepository.ListAsync();
            }
        }

        public async Task<ValidationError[]> UpdateAdminAsync(Admin admin, string originator)
        {
            using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
            {
                if (!admin.Name.ToLowerInvariant().Contains(@"qubiz\"))
                    admin.Name = @"QUBIZ\" + admin.Name;

                Admin dbAdmin = await unitOfWork.AdminRepository.GetByIDAsync(admin.ID);
                if (string.Compare(dbAdmin.Name, originator, true) == 0)
                    return new ValidationError[1] { new ValidationError() { Message = "You cannot edit yourself!" } };

                dbAdmin = await unitOfWork.AdminRepository.GetByNameAsync(admin.Name);
                if (dbAdmin != null && dbAdmin.ID != admin.ID)
                    return new ValidationError[1] { new ValidationError() { Message = "Name already exists!" } };

                dbAdmin = await unitOfWork.AdminRepository.GetByIDAsync(admin.ID);

                dbAdmin.Name = admin.Name.Substring(0, 6).ToUpper() + admin.Name.Substring(6);

                unitOfWork.AdminRepository.Update(dbAdmin);

                await unitOfWork.SaveAsync();

                return new ValidationError[0];
            }
        }
    }
}
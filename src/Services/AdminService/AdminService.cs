using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Database.Repositories;
using Qubiz.QuizEngine.Database;
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
                if (originator == admin.Name)
                    return new ValidationError[1] { new ValidationError() { Message = "Name already exists!" } };

                admin.ID = Guid.NewGuid();

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
                return await unitOfWork.AdminRepository.GetAllAdminsAsync();
            }

        }

        public async Task<ValidationError[]> UpdateAdminAsync(Admin admin, string originator)
        {
            using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
            {
                if (originator == admin.Name)
                    return new ValidationError[1] { new ValidationError() { Message = "You can't change yourself!" } };

                Admin dbAdmin = await unitOfWork.AdminRepository.GetByIDAsync(admin.ID);

                Mapper.Map(admin, dbAdmin);

                unitOfWork.AdminRepository.Update(dbAdmin);

                await unitOfWork.SaveAsync();

                return new ValidationError[0];
            }
        }
    }
}
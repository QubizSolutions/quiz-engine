﻿using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Database.Repositories;
using Qubiz.QuizEngine.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly IConfig config;

        public AdminService(IConfig config)
        {
            this.config = config;
        }

        public async Task<ValidationError[]> AddAdminAsync(Admin admin, string originator)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(config))
            {
                if (!admin.Name.Contains(@"QUBIZ\") && !admin.Name.Contains(@"qubiz\") && !admin.Name.Contains(@"Qubiz\"))
                {
                    admin.Name = @"QUBIZ\" + admin.Name;
                }

                Admin adminUser = await unitOfWork.AdminRepository.GetByNameAsync(admin.Name);
                if (adminUser != null)
                    return new ValidationError[1] { new ValidationError() { Message = "Name already exists!" } };

                unitOfWork.AdminRepository.Create(admin);
                await unitOfWork.SaveAsync();

                return new ValidationError[0];
            }
        }

        public async Task<ValidationError[]> DeleteAdminAsync(Guid id, string originator)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(config))
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
            using (IUnitOfWork unitOfWork = new UnitOfWork(config))
            {
                return await unitOfWork.AdminRepository.GetByIDAsync(id);
            }
        }

        public async Task<Admin[]> GetAllAdminsAsync()
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(config))
            {
                return await unitOfWork.AdminRepository.GetAllAdminsAsync();
            }
        }

        public async Task<ValidationError[]> UpdateAdminAsync(Admin admin, string originator)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(config))
            {
                if (admin.Name.Substring(0, 6) != @"QUBIZ\")
                {
                    admin.Name = @"QUBIZ\" + admin.Name;
                }

            if(admin.Name == originator)
            {
                return new ValidationError[1] { new ValidationError() { Message = "You cannot edit yourself!" } };
            }
                Admin adminUser = await unitOfWork.AdminRepository.GetByNameAsync(admin.Name);
                if (adminUser != null)
                    return new ValidationError[1] { new ValidationError() { Message = "User already exists!" } };

                Admin dbAdmin = await unitOfWork.AdminRepository.GetByIDAsync(admin.ID);
                Mapper.Map(admin, dbAdmin);
                unitOfWork.AdminRepository.Update(dbAdmin);
                await unitOfWork.SaveAsync();

                return new ValidationError[0];
            }
        }
    }
}
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using Qubiz.QuizEngine.Repositories;
using System.Web.Http;
using Qubiz.QuizEngine.Core;
using Qubiz.QuizEngine.Infrastructure.Logger;
using Qubiz.QuizEngine.Database;
using Qubiz.QuizEngine.Infrastructure;
using Qubiz.QuizEngine.Database.Repositories;
using System;
using Qubiz.QuizEngine.Services.AdminService;

namespace Qubiz.QuizEngine
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IConfig, Config>();
            container.RegisterType<System.Data.Entity.DbContext, QuizEngineDataContext>(new InjectionConstructor(container.Resolve<IConfig>().ConnectionString));
            container.RegisterType<IRepository, RepositoryBase>();
            container.RegisterType<Repositories.IAdminRepository, Repositories.AdminRepository>();
            container.RegisterType<Repositories.IQuestionRepository, Repositories.QuestionRepository>();
            container.RegisterType<Repositories.ITestRepository, Repositories.TestRepository>();
            container.RegisterType<Repositories.IExamRepository, Repositories.ExamRepository>();
            container.RegisterType<IExamService, ExamService>();
            container.RegisterType<ILogger,EventViewerLogger>();
			container.RegisterType<Repositories.IFeatureFlagRepository, Repositories.FeatureFlagRepository>();


            //container.RegisterType<Database.Repositories.IBaseRepository, Database.Repositories.BaseRepository>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IAdminService, AdminService>();
            container.RegisterType<Database.Repositories.IAdminRepository, Database.Repositories.AdminRepository>();
        }
    }
}
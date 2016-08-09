using Microsoft.Practices.Unity;
using Qubiz.QuizEngine.Core;
using Qubiz.QuizEngine.Database;
using Qubiz.QuizEngine.Infrastructure;
using Qubiz.QuizEngine.Infrastructure.Logger;
using Qubiz.QuizEngine.Repositories;
using Qubiz.QuizEngine.Services.AdminService;
using Qubiz.QuizEngine.Services.SectionService;
using System.Web.Http;
using System.Web.Mvc;
using Unity.Mvc4;

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
            container.RegisterType<IAdminRepository, AdminRepository>();
            container.RegisterType<IQuestionRepository, QuestionRepository>();
            container.RegisterType<ITestRepository, TestRepository>();
            container.RegisterType<IExamRepository, ExamRepository>();
            container.RegisterType<IExamService, ExamService>();
            container.RegisterType<ILogger, EventViewerLogger>();
            container.RegisterType<Repositories.IFeatureFlagRepository, Repositories.FeatureFlagRepository>();


            // M
            container.RegisterType<Database.Repositories.IAdminRepository, Database.Repositories.AdminRepository>();
            container.RegisterType<Services.IQuestionService, Services.QuestionService>();
            container.RegisterType<IAdminService, AdminService>();

			container.RegisterType<ISectionService, SectionService>();
		}
    }
}
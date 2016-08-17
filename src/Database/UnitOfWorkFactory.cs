using Qubiz.QuizEngine.Database.Repositories;
using Qubiz.QuizEngine.Infrastructure;

namespace Qubiz.QuizEngine.Database
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private IConfig config;

        public UnitOfWorkFactory()
        {
        }

        public UnitOfWorkFactory(IConfig config)
        {
            this.config = config;
        }

        public IUnitOfWork Create()
        {
            return new UnitOfWork(config);
        }
        public static UnitOfWorkFactory factory = new UnitOfWorkFactory();
    }
}
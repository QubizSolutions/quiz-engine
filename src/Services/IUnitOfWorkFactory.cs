using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qubiz.QuizEngine.Database.Repositories;
using Qubiz.QuizEngine.Infrastructure;

namespace Qubiz.QuizEngine.Services
{
    public interface IUnitOfWorkFactory
    {
    IUnitOfWork Create();
    }
}
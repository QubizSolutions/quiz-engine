using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Infrastructure
{
    public interface IConfig
    {
        string ConnectionString { get; }
    }
}

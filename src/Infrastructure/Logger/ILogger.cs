using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Qubiz.QuizEngine.Infrastructure.Logger
{
    public interface ILogger
    {
        void LogException(Exception ex);
    }
}
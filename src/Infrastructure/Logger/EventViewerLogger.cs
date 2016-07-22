using Qubiz.QuizEngine.Infrastructure.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Qubiz.QuizEngine.Infrastructure.Logger
{
    public class EventViewerLogger : ILogger
    {
        public void LogException(Exception ex)
        {
            LogHelper.LogError(ex);
        }
    }
}
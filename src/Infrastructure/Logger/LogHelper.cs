using System;
using System.Text;
using System.Diagnostics;

namespace Qubiz.QuizEngine.Infrastructure.Logger
{
    /*
     * If the apppool runs under a non-admin user, you will need to create manualy the event source for the event log:
     * 1. Run the power shell console as an administrator     
     * 2. Run this command: New-EventLog -LogName Application -Source "QUIZ ENGINE"
     */
    public static class LogHelper
    {
        private const string SOURCE = "QUIZ ENGINE";

        public static void LogError(string message)
        {
            try
            {
                CreateEventSource();
                EventLog.WriteEntry(SOURCE, message, EventLogEntryType.Error);
            }
            catch { }
        }

        public static void LogError(Exception exception)
        {
            try
            {
                LogError(ExceptionToString(exception));
            }
            catch { }
        }

        public static void LogInfo(string message)
        {
            try
            {
                CreateEventSource();
                EventLog.WriteEntry(SOURCE, message, EventLogEntryType.Information);
            }
            catch { }
        }

        public static string ExceptionToString(Exception exception)
        {
            StringBuilder buffer = new StringBuilder();
            string label = "==EXCEPTION==============================================================";

            while (exception != null)
            {
                buffer.AppendLine(label);
                buffer.AppendLine("[Exception type] " + exception.GetType().FullName);
                buffer.AppendLine("[Message] " + exception.Message);
                buffer.AppendLine("[Source] " + exception.Source);
                buffer.AppendLine("[Stack trace] " + exception.StackTrace);
                buffer.AppendLine("[Target site] " + exception.TargetSite);

                exception = exception.InnerException;
                label = "===============INNER EXCEPTION==============================================";
            }

            return buffer.ToString();
        }

        private static void CreateEventSource()
        {
            if (!EventLog.SourceExists(SOURCE))
                EventLog.CreateEventSource(SOURCE, null);
        }
    }
}
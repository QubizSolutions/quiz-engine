using Qubiz.QuizEngine.Infrastructure;
using System.Configuration;

namespace Qubiz.QuizEngine
{
    public class Config : IConfig
    {
        private string _ConnectionString;
        public string ConnectionString
        {
            get
            {
                if (_ConnectionString == null)
                    _ConnectionString = ConfigurationManager.ConnectionStrings["qubizQuizEngine"].ConnectionString;

                return _ConnectionString;
            }
        }
    }
}

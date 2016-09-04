using Qubiz.QuizEngine.Infrastructure;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Qubiz.QuizEngine.UnitTesting.Database
{
    public class Config : IConfig
    {
        private string _ConnectionString;
        public string ConnectionString
        {
            get
            {
                if (_ConnectionString == null)
                    _ConnectionString = ConfigurationManager.ConnectionStrings["qubizQuizEngineTest"].ConnectionString;

                return _ConnectionString;
            }
        }
    }
}

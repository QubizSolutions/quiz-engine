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
                _ConnectionString = ConfigurationManager.ConnectionStrings["qubizQuizEngineTest"].ConnectionString;
                if (_ConnectionString == null) {
                    string localConfigFilePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", ""), @"..\..\..\")) + "config.local";

                    if (File.Exists(localConfigFilePath))
                    {
                        XDocument moduleConfig = XDocument.Load(localConfigFilePath);

                        _ConnectionString = moduleConfig.XPathSelectElements("/configuration/test").Select(x => x.Attribute("connectionString").Value).First();
                    }
                }
                return _ConnectionString;
            }
        }
    }
}

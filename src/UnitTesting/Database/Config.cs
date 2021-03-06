﻿using Qubiz.QuizEngine.Infrastructure;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Qubiz.QuizEngine.UnitTesting.Database
{
    public class Config : IConfig
    {
        public string ConnectionString
        {
            get
            {
                string localConfigFilePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", ""), @"..\..\..\")) + "config.local";

                if (File.Exists(localConfigFilePath))
                {
                    XDocument moduleConfig = XDocument.Load(localConfigFilePath);

                    return moduleConfig.XPathSelectElements("/configuration/test").Select(x => x.Attribute("connectionString").Value).First();
                }

                return ConfigurationManager.ConnectionStrings["qubizQuizEngineTest"].ConnectionString;
            }
        }
    }
}
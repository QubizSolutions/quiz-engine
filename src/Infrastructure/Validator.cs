using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Infrastructure
{
    public class Validator
    {
        public string Message { get; set; }
        public Validator(string message)
        {
            this.Message = message;
        }
    }
}

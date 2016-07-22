using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Qubiz.QuizEngine.ViewModels
{
    public class QuestionListItem
    {
        public Guid ID { get; set; }
        public int Number { get; set; }
        public string Section { get; set; }
    }
}
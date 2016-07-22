using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Qubiz.QuizEngine.ViewModels
{
    public class TestListItem
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public bool IsPublished { get; set; }      
    }
}
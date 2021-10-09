using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models.ViewModel
{
    public class QuizDetailsVM
    {
     
        public QuizDetails QuizDetails { get; set; }
        public Question Question { get; set; }
        public List<string> AnswerName { get; set; } 
        public Answer Answer{ get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zh.Persistence;

namespace Zh.WebSite.Models
{
    public class IndexViewModel
    {
        public List<Question> QuestionList { get; set; }

        public List<Answer> AnswerList { get; set; }
    }
}

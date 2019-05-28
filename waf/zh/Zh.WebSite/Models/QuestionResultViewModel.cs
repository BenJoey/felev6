using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zh.Persistence;

namespace Zh.WebSite.Models
{
    public class QuestionResultViewModel
    {
        public string QuestionText { get; set; }

        public List<Answer> Answers { get; set; }
        public List<int> VoteCounts { get; set; }
    }
}

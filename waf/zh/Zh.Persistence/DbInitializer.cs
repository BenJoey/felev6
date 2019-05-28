using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Clauses.ResultOperators;

namespace Zh.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(ZhContext context)
        {

            if (context.Questions.Any())
            {
                return;
            }

            var q1 = new Question()
            {
                DueTime = DateTime.Parse("2019-05-29 10:00:00"),
                Subject = "Az elet ertelme?"
            };
            var q2 = new Question()
            {
                DueTime = DateTime.Parse("2019-05-20 10:00:00"),
                Subject = "Miert jo a WAF?"
            };
            context.Questions.Add(q1);
            context.Questions.Add(q2);

            var ans1 = new Answer()
            {
                AnswerText = "42",
                Question = q1
            };
            var ans2 = new Answer()
            {
                AnswerText = "semmi",
                Question = q1
            };
            var ans3 = new Answer()
            {
                AnswerText = "nem tudom",
                Question = q1
            };
            var ans4 = new Answer()
            {
                AnswerText = "implementacio fuggo",
                Question = q1
            };

            var ans5 = new Answer()
            {
                AnswerText = "hasznos",
                Question = q2
            };
            var ans6 = new Answer()
            {
                AnswerText = "ne unatkozzunk",
                Question = q2
            };
            var ans7 = new Answer()
            {
                AnswerText = "keves webes targy van",
                Question = q2
            };
            var ans8 = new Answer()
            {
                AnswerText = "valamiert biztos",
                Question = q2
            };

            context.Answers.Add(ans1);
            context.Answers.Add(ans2);
            context.Answers.Add(ans3);
            context.Answers.Add(ans4);
            context.Answers.Add(ans5);
            context.Answers.Add(ans6);
            context.Answers.Add(ans7);
            context.Answers.Add(ans8);

            context.SaveChanges();
        }
    }
}

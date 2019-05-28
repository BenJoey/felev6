using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zh.Persistence;
using Zh.WebSite.Models;

namespace Zh.WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ZhContext _context;

        public HomeController(ZhContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var questions = await _context.Questions.Where(
                q => (q.DueTime + TimeSpan.FromHours(24))>DateTime.Now).ToListAsync();
            var answers = await _context.Answers.Where(a => questions.Contains(a.Question)).ToListAsync();

            var indexVM = new IndexViewModel()
            {
                AnswerList = answers,
                QuestionList = questions
            };
            return View(indexVM);
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateQViewModel createQViewModel)
        {
            if (!ModelState.IsValid)
                return View("Create", createQViewModel);

            var question = new Question()
            {
                DueTime = createQViewModel.QuestionDueTime,
                Subject = createQViewModel.QuestionText
            };
            _context.Questions.Add(question);

            _context.Answers.AddRange(
                new Answer()
                {
                    AnswerText = createQViewModel.Answer1,
                    Question = question
                },
                new Answer()
                {
                    AnswerText = createQViewModel.Answer2,
                    Question = question
                },
                new Answer()
                {
                    AnswerText = createQViewModel.Answer3,
                    Question = question
                },
                new Answer()
                {
                    AnswerText = createQViewModel.Answer4,
                    Question = question
                });

            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Vote(int questionId, int answerId)
        {
            var question = _context.Questions.Where(m => m.Id == questionId).FirstOrDefault();
            var answer = _context.Answers.FirstOrDefault(m => m.Id == answerId);
            if (question == null || question.DueTime < DateTime.Now || answer == null)
                return RedirectToAction("Index", "Home");
            _context.Votes.Add(new Vote()
            {
                Answer = answer,
                Question = question
            });
            _context.SaveChanges();
            return RedirectToAction("Index", "Home"); ;
        }

        public IActionResult Results(int? id)
        {
            var question = _context.Questions.FirstOrDefault(m => m.Id == id);
            if(question == null)
                return RedirectToAction("Index", "Home");

            var votes = _context.Votes.Where(m => m.Question == question).ToList();
            var answers = _context.Answers.Where(m => m.Question == question).ToList();
            var votecounts = answers.Select(m => votes.Count(v => v.Answer == m)).ToList();
            var resVM = new QuestionResultViewModel()
            {
                QuestionText = question.Subject,
                Answers = answers,
                VoteCounts = votecounts
            };
            
            return View(resVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

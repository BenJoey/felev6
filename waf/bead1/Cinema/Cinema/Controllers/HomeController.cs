using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace Cinema.Controllers
{
    public class HomeController : Controller
    {
        private readonly CinemaContext _context;

        public HomeController(CinemaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var movies = (from m in _context.Movies orderby m.Modified descending select m).Take(5);
            var shows = from m in _context.Shows where m.StartTime.Day == DateTime.Now.Day select m;
            var rooms = from m in _context.Rooms select m;
            var movieVm = new MovieVm()
            {
                Films = await movies.ToListAsync(),
                ShowTimes = await shows.ToListAsync(),
                Rooms = await rooms.ToListAsync()
            };
            return View(movieVm);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

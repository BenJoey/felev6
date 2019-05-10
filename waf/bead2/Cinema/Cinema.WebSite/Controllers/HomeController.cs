using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cinema.Persistence;
using Cinema.WebSite.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.WebSite.Controllers
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
            var shows = from m in _context.Shows orderby m.StartTime where m.StartTime.Day == DateTime.Now.Day && m.StartTime > DateTime.Now select m;
            var rooms = from m in _context.Rooms select m;
            var movieVm = new MovieIndexViewModel()
            {
                Films = await movies.ToListAsync(),
                ShowTimes = await shows.ToListAsync(),
                Rooms = await rooms.ToListAsync()
            };
            return View(movieVm);
        }

        public FileResult ImageForMovie(Int32? movieId)
        {
            if (movieId == null)
                return File("~/images/NoImage.png", "image/png");

            Byte[] imageContent = _context.Posters
                .Where(image => image.MovieRefId == movieId)
                .Select(image => image.Image)
                .FirstOrDefault();

            if (imageContent == null)
                return File("~/images/NoImage.png", "image/png");

            return File(imageContent, "image/png");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

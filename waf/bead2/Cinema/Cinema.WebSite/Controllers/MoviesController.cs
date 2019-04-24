using System;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Persistence;
using Cinema.WebSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema.WebSite.Controllers
{
    public class MoviesController : Controller
    {
        private readonly CinemaContext _context;

        public MoviesController(CinemaContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var movies = from m in _context.Movies select m;
            var movieVm = new MovieIndexViewModel()
            {
                Films = await movies.ToListAsync()
            };
            return View(movieVm);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            var thisMovieShows = from m in _context.Shows orderby m.StartTime where m.Movie.Id == id && m.StartTime > DateTime.Now select m;
            var rooms = from m in _context.Rooms select m;
            var movieDvm = new MovieDetailsViewModel()
            {
                Film = movie,
                ShowTimes = await thisMovieShows.ToListAsync(),
                Rooms = await rooms.ToListAsync()
            };

            return View(movieDvm);
        }

        public async Task<IActionResult> Reserve(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selectedShow = await _context.Shows
                .FirstOrDefaultAsync(m => m.Id == id);
            if (selectedShow == null)
            {
                return NotFound();
            }

            var thisShowRoom = await _context.Rooms
                .FirstOrDefaultAsync(m => m.Id == selectedShow.RoomRefId);
            if (thisShowRoom == null)
            {
                return NotFound();
            }

            var thisShowSeats = from m in _context.Seats where m.ShowRefId == id select m;
            var reserveVm = new ReservationViewModel()
            {
                ShowId = id.Value,
                Room = thisShowRoom,
                Seats = await thisShowSeats.ToListAsync()
            };
            return View(reserveVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserve(ReservationViewModel reservationViewModel)
        {
            if (ModelState.IsValid)
            {
                var ids = reservationViewModel.SeatIds.Split(",");
                var selectedSeats = from m in _context.Seats where ids.Contains(m.Id.ToString()) select m;
                foreach (var current in selectedSeats)
                {
                    if (current.State != State.Free)
                    {
                        return NotFound();
                    }

                    current.State = State.Reserved;
                    current.NameReserved = reservationViewModel.Name;
                    current.PhoneNum = reservationViewModel.PhoneNumber;
                }
                _context.UpdateRange(selectedSeats);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(HomeController.Index));
            }
            var selectedShow = await _context.Shows
                .FirstOrDefaultAsync(m => m.Id == reservationViewModel.ShowId);
            if (selectedShow == null)
            {
                return NotFound();
            }

            var thisShowRoom = await _context.Rooms
                .FirstOrDefaultAsync(m => m.Id == selectedShow.RoomRefId);
            if (thisShowRoom == null)
            {
                return NotFound();
            }

            var thisShowSeats = from m in _context.Seats where m.ShowRefId == reservationViewModel.ShowId
                select m;
            reservationViewModel.Room = thisShowRoom;
            reservationViewModel.Seats = await thisShowSeats.ToListAsync();
            reservationViewModel.SeatIds = "";
            return View(reservationViewModel);
        }
    }
}

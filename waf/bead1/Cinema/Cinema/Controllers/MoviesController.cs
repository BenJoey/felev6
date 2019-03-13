using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cinema.Models;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.ProjectModel;
using Remotion.Linq.Clauses;

namespace Cinema.Controllers
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
            var movieVm = new MovieVm()
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

            var thisMovieShows = from m in _context.Shows orderby m.StartTime where m.Movie.Id == id select m;
            var rooms = from m in _context.Rooms select m;
            var movieDvm = new MovieDetailsVm()
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
            var reserveVm = new Reservation()
            {
                ShowId = id.Value,
                Room = thisShowRoom,
                Seats = await thisShowSeats.ToListAsync()
            };
            return View(reserveVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserve(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                var ids = reservation.SeatIds.Split(",");
                var selectedSeats = from m in _context.Seats where ids.Contains(m.Id.ToString()) select m;
                foreach (var current in selectedSeats)
                {
                    if (current.State != State.Free)
                    {
                        return NotFound();
                    }

                    current.State = State.Reserved;
                    current.NameReserved = reservation.Name;
                    current.PhoneNum = reservation.Phone;
                }
                _context.UpdateRange(selectedSeats);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(HomeController.Index));
            }
            var selectedShow = await _context.Shows
                .FirstOrDefaultAsync(m => m.Id == reservation.ShowId);
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

            var thisShowSeats = from m in _context.Seats where m.ShowRefId == reservation.ShowId
                select m;
            reservation.Room = thisShowRoom;
            reservation.Seats = await thisShowSeats.ToListAsync();
            reservation.SeatIds = "";
            return View(reservation);
        }
    }
}

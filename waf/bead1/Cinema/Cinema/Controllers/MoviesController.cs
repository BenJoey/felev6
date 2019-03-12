using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cinema.Models;

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

            var thisMovieShows = from m in _context.Shows where m.Movie.Id == id select m;
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
            var reserveVm = new ReserveVm()
            {
                ShowsRoom = thisShowRoom,
                ShowSeats = await thisShowSeats.ToListAsync()
            };
            return View(reserveVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserve([Bind("name, phone, seatids")]ReserveVm input)
        {
            if (ModelState.IsValid)
            {
                var seatIDs = input.seatids.Split(',');
                foreach (var current in seatIDs)
                {
                    var id = Convert.ToInt32(current);
                    var seat = await _context.Seats
                        .FirstOrDefaultAsync(m => m.Id == id);
                    seat.State = State.Reserved;
                    seat.NameReserved = input.Name;
                    seat.PhoneNum = input.Phone;
                    _context.Update(seat);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(input);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Director,Length,Description,Modified")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Director,Length,Description,Modified")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}

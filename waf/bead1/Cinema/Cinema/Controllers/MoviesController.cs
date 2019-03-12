﻿using System;
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
            var reserveVm = new ReserveData()
            {
                Room = thisShowRoom,
                Seats = await thisShowSeats.ToListAsync()
            };
            ViewBag.Data = reserveVm;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserve(int id, Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                var seatIDs = reservation.SeatIds.Split(',');
                foreach (var current in seatIDs)
                {
                    var curId = Convert.ToInt32(current);
                    var seat = await _context.Seats
                        .FirstOrDefaultAsync(m => m.Id == curId);
                    seat.State = State.Reserved;
                    seat.NameReserved = reservation.Name;
                    seat.PhoneNum = reservation.Phone;
                    _context.Update(seat);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }
    }
}

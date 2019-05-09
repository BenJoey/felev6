using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Persistence;
using Cinema.Persistence.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Show")]
    public class ShowController : Controller
    {
        private readonly CinemaContext _context;

        public ShowController(CinemaContext context)
        {
            this._context = context;
        }

        // POST: api/Movie
        [HttpPost("NewShow")]
        public IActionResult NewShow([FromBody] ShowDto item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var selectedMovie = (_context.Movies.Where(o => o.Id == item.movieId)).FirstOrDefault();
                    var selectedRoom = (_context.Rooms.Where(o => o.Id == item.roomId)).FirstOrDefault();
                    if (selectedMovie == null || selectedRoom == null)
                        return NotFound();
                    var newShow = new Show()
                    {
                        Movie = selectedMovie,
                        Room = selectedRoom,
                        StartTime = DateTime.Parse(item.StarTime)
                    };

                    _context.Shows.Add(newShow);

                    _context.SaveChanges();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("MovieList")]
        public IEnumerable<Movie> MovieList()
        {
            IQueryable<Movie> movies = _context.Movies
                .OrderBy(l => l.Title);

            return movies;
        }

        [HttpGet("RoomList")]
        public IEnumerable<Room> RoomList()
        {
            IQueryable<Room> rooms = _context.Rooms
                .OrderBy(l => l.RoomName);

            return rooms;
        }
    }
}
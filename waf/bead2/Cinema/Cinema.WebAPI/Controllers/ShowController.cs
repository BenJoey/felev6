using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Persistence;
using Cinema.Persistence.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema.WebAPI.Controllers
{
    [Route("api/Show")]
    public class ShowController : Controller
    {
        private readonly CinemaContext _context;

        public ShowController(CinemaContext context)
        {
            this._context = context;
        }

        // POST: api/Show
        [HttpPost]
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

                    var newShowStart = DateTime.Parse(item.StartTime);
                    var newShowEnd = newShowStart + selectedMovie.Length + TimeSpan.FromMinutes(15);
                    var showsToCheck = (_context.Shows.Where(o => o.RoomRefId == selectedRoom.Id)).ToList();
                    foreach (var show in showsToCheck)
                    {
                        var currentMovie = (_context.Movies.Where(o => o.Id == show.MovieRefId)).FirstOrDefault();
                        var showEnd = show.StartTime + currentMovie.Length + TimeSpan.FromMinutes(15);
                        var invalidTime =
                            (DateTime.Compare(show.StartTime, newShowStart) < 1 && DateTime.Compare(newShowEnd,
                                 showEnd) < 1) || (DateTime.Compare(newShowStart, show.StartTime) == -1 &&
                                                   DateTime.Compare(newShowEnd, show.StartTime) == 1);
                            
                        if (invalidTime)
                        {
                            return BadRequest();
                        }
                    }
                    var newShow = new Show()
                    {
                        Movie = selectedMovie,
                        Room = selectedRoom,
                        StartTime = DateTime.Parse(item.StartTime)
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
        public IActionResult MovieList()
        {
            try
            {
                return Ok(_context.Movies.ToList().Select(mov => new MovieDto
                {
                    Id = mov.Id,
                    Title = mov.Title
                }));
            }
            catch
            {
                // Internal Server Error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            /*IQueryable<Movie> movies = _context.Movies
                .OrderBy(l => l.Title);

            return Ok(movies);*/
        }

        [HttpGet("RoomList")]
        public IActionResult RoomList()
        {
            try
            {
                return Ok(_context.Rooms.ToList().Select(room => new RoomDto
                {
                    Id = room.Id,
                    RoomName = room.RoomName
                }));
            }
            catch
            {
                // Internal Server Error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            /*IQueryable<Room> rooms = _context.Rooms
                .OrderBy(l => l.RoomName);

            return Ok(rooms);*/
        }
    }
}
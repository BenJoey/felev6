using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Persistence;
using Cinema.Persistence.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ReservationController : Controller
    {
        private readonly CinemaContext _context;

        public ReservationController(CinemaContext context)
        {
            this._context = context;
        }

        [HttpGet("ShowList")]
        public IActionResult ShowList()
        {
            try
            {
                return Ok(_context.Shows.Where(o => o.StartTime > DateTime.Now).ToList().Select(show => new ShowDto
                {
                    showId = show.Id,
                    movieName = show.Movie.Title,
                    StartTime = show.StartTime.ToString("F")
                }));
            }
            catch
            {
                // Internal Server Error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
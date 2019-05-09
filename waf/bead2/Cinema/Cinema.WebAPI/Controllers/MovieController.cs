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
    [Route("api/Movie")]
    public class ItemsController : Controller
    {
        private readonly CinemaContext _context;

        public ItemsController(CinemaContext context)
        {
            this._context = context;
        }

        // POST: api/Items
        [HttpPost("[action]")]
        public IActionResult New([FromBody] MovieDto item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_context.Movies.Any(i => i.Title.Equals(item.Title)))
                        return BadRequest();

                    var newMovie = new Movie()
                    {
                        Description = item.Description,
                        Title = item.Title,
                        Length = item.Length
                    };

                    _context.Movies.Add(newMovie);

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
    }
}
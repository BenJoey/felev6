﻿using System;
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
    [Route("api/Movie")]
    public class MovieController : Controller
    {
        private readonly CinemaContext _context;

        public MovieController(CinemaContext context)
        {
            _context = context;
        }

        // POST: api/Movie
        [HttpPost]
        [Authorize]
        public IActionResult NewMovie([FromBody] MovieDto item)
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
                        Length = TimeSpan.Parse(item.Length),
                        Director = item.Director
                    };

                    _context.Movies.Add(newMovie);

                    _context.Posters.Add(new Poster
                    {
                        Movie = newMovie,
                        Image = item.Poster
                    });

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

        [HttpGet]
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
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Persistence;
using Cinema.Persistence.DTOs;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SellTickets([FromBody] ReservationDto newReserve)
        {
            try
            {
                var seats = _context.Seats.Where(o => newReserve.SelectedSeats.Contains(o.Id));
                foreach (var seat in seats)
                {
                    seat.State = State.Sold;
                    seat.NameReserved = newReserve.Name;
                    seat.PhoneNum = newReserve.PhoneNum;
                }
                _context.UpdateRange(seats);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                BadRequest();
            }
            return Ok();
        }

        [HttpGet("ShowList")]
        public IActionResult ShowList()
        {
            try
            {
                return Ok(_context.Shows.Where(o => o.StartTime > DateTime.Now).OrderBy(o => o.StartTime).ToList().Select(show => new ShowDto
                {
                    showId = show.Id,
                    movieName = _context.Movies.Where(o => o.Id == show.MovieRefId).Select(o => o.Title).FirstOrDefault(),
                    StartTime = show.StartTime.ToString("F")
                }));
            }
            catch
            {
                // Internal Server Error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("SeatList/{id}")]
        public IActionResult SeatList(int id)
        {
            try
            {
                return Ok(_context.Seats.Where(o => o.ShowRefId == id).ToList().Select(seat => new SeatDto
                {
                    Id = seat.Id,
                    Row = seat.Row+1,
                    Col = seat.Col+1,
                    NameReserved = seat.NameReserved,
                    PhoneNum = seat.PhoneNum,
                    State = seat.State.ToString()
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
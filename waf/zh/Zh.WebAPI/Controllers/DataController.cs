using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zh.Persistence;
using Zh.Persistence.DTOs;

namespace Zh.WebAPI.Controllers
{
    [Route("api/Data")]
    public class DataController : Controller
    {
        private readonly ZhContext _context;

        public DataController(ZhContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        // [Authorize]
        public IActionResult NewMovie([FromBody] DataDto item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_context.Datas.Any(i => i.Name.Equals(item.Name)))
                        return BadRequest();

                    var newData = new Data()
                    {
                        Name = item.Name
                    };

                    _context.Datas.Add(newData);

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
        public IActionResult DataList()
        {
            try
            {
                return Ok(_context.Datas.ToList().Select(d => new DataDto
                {
                    Id = d.Id,
                    Name = d.Name
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
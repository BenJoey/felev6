using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowController : Controller
    {
        private readonly CinemaContext _context;

        public ShowController(CinemaContext context)
        {
            this._context = context;
        }
    }
}
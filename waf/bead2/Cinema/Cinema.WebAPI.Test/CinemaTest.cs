using System;
using System.Collections.Generic;
using System.Linq;
using Cinema.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Cinema.WebAPI.Test
{
    public class CinemaTest : IDisposable
    {
        private readonly CinemaContext _context;
        private readonly List<Movie> _movies;
        private readonly List<Show> _shows;
        private readonly List<Room> _rooms;

        public CinemaTest()
        {
            var options = new DbContextOptionsBuilder<CinemaContext>()
                .UseInMemoryDatabase("CinemaTest")
                .Options;

            _context = new CinemaContext(options);
            _context.Database.EnsureCreated();
            var roomData = new List<Room>
            {
                new Room
                {
                    RoomName = "TEST_ROOM",
                    NumOfCols = 2,
                    NumOfRows = 2
                }
            };
            _context.Rooms.AddRange(roomData);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Cinema.Persistence;
using Cinema.Persistence.DTOs;
using Cinema.WebAPI.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Cinema.WebAPI.Test
{
    public class CinemaTest : IDisposable
    {
        private readonly CinemaContext _context;
        private readonly List<MovieDto> _movieDTOs;
        private readonly List<ShowDto> _showDTOs;
        private readonly List<SeatDto> _seatDTOs;
        private readonly List<RoomDto> _roomDTOs;
        // private readonly List<ReservationDto> _reservationDtos;

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
                    NumOfRows = 1
                }
            };
            _context.Rooms.AddRange(roomData);

            var movieData = new List<Movie>
            {
                new Movie
                {
                    Title = "TEST_MOVIE_1",Description = "Only used for test purposes.",Director = "xUnit",Length = TimeSpan.Parse("00:10:00")
                },
                new Movie
                {
                    Title = "TEST_MOVIE_2",Description = "Only used for test purposes.",Director = "xUnit2",Length = TimeSpan.Parse("00:15:00")
                },
                new Movie
                {
                    Title = "TEST_MOVIE_3",Description = "Only used for test purposes.",Director = "xUnit3",Length = TimeSpan.Parse("00:20:00")
                }
            };
            _context.Movies.AddRange(movieData);

            var showData = new List<Show>
            {
                new Show
                {
                    Movie = movieData[0], Room = roomData[0], StartTime = DateTime.Parse("2020-01-01 12:00:00")
                },
                new Show
                {
                    Movie = movieData[2], Room = roomData[0], StartTime = DateTime.Parse("2020-02-01 12:30:00")
                }
            };
            _context.Shows.AddRange(showData);

            var seatData = new List<Seat>
            {
                new Seat {Col = 0, Row = 0, Room = roomData[0], Show = showData[0], State = State.Free},
                new Seat {Col = 1, Row = 0, Room = roomData[0], Show = showData[0], State = State.Free},
                new Seat {Col = 0, Row = 0, Room = roomData[0], Show = showData[1], State = State.Free},
                new Seat {Col = 1, Row = 0, Room = roomData[0], Show = showData[1], State = State.Free}
            };
            _context.Seats.AddRange(seatData);
            _context.SaveChanges();

            _movieDTOs = movieData.Select(movie => new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title
            }).ToList();

            _showDTOs = showData.Select(show => new ShowDto
            {
                showId = show.Id,
                movieName = _movieDTOs.Single(movie => movie.Id == show.MovieRefId).Title,
                StartTime = show.StartTime.ToString("F")
            }).ToList();

            _roomDTOs = roomData.Select(room => new RoomDto
            {
                Id = room.Id,
                RoomName = room.RoomName
            }).ToList();

            _seatDTOs = seatData.Select(seat => new SeatDto
            {
                Col = seat.Col,
                Row = seat.Row,
                State = seat.State.ToString(),
                NameReserved = seat.NameReserved,
                PhoneNum = seat.PhoneNum
            }).ToList();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public void GetMoviesTest()
        {
            var controller = new MovieController(_context);
            var result = controller.MovieList();

            var objectResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<MovieDto>>(objectResult.Value);
            Assert.Equal(_movieDTOs, model);
        }

        [Fact]
        public void GetShowsTest()
        {
            var controller = new ShowController(_context);
            var result = controller.ShowList();

            var objectResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ShowDto>>(objectResult.Value);
            Assert.Equal(_showDTOs, model);
        }

        [Fact]
        public void GetRoomsTest()
        {
            var controller = new ShowController(_context);
            var result = controller.RoomList();

            var objectResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<RoomDto>>(objectResult.Value);
            Assert.Equal(_roomDTOs, model);
        }

        [Fact]
        public void AddNewMovieTest()
        {
            var newMovie = new MovieDto
            {
                Title = "NEW_MOVIE",
                Description = "New movie to add",
                Director = "TEST_CASE",
                Length = "00:30:00",
                Poster = new Byte[] { 1, 1, 4, 7, 8, 0, 1 }
            };

            var controller = new MovieController(_context);
            var result = controller.NewMovie(newMovie);

            var objectResult = Assert.IsType<OkResult>(result);

            Assert.Equal(_movieDTOs.Count + 1, _context.Movies.Count());
            Assert.Equal(200,objectResult.StatusCode);
        }

        [Fact]
        public void AddNewShowTest()
        {
            var newShow = new ShowDto
            {
                movieId = _context.Movies.Single(o => o.Title == "TEST_MOVIE_2").Id,
                roomId = _context.Rooms.Single(o => o.RoomName == "TEST_ROOM").Id,
                StartTime = "2019-12-20 10:00:00"
            };

            var controller = new ShowController(_context);
            var result = controller.NewShow(newShow);

            var objectResult = Assert.IsType<OkResult>(result);

            Assert.Equal(_showDTOs.Count + 1, _context.Shows.Count());
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public async void ReservationTest()
        {
            var newReservation = new ReservationDto
            {
                Name = "TEST_CUSTOMER",
                PhoneNum = "061555222",
                SelectedSeats = _context.Seats.Where(o => o.State == State.Free).Select(o => o.Id).Take(2).ToList()
            };

            // ITestOutputHelper a = new TestOutputHelper();//(newReservation);
            // a.WriteLine(newReservation.SelectedSeats.ToString());

            var controller = new ReservationController(_context);
            var result = await controller.SellTickets(newReservation);

            var objectResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, objectResult.StatusCode);
            Assert.Equal(_seatDTOs.Count, _context.Seats.Count(o => o.State == State.Free) + 2);
            foreach (var seatId in newReservation.SelectedSeats)
            {
                Assert.Equal(State.Sold, _context.Seats.Single(o => o.Id == seatId).State);
            }
        }
    }
}

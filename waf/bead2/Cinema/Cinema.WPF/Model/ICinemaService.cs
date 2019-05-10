using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Persistence;
using Cinema.Persistence.DTOs;

namespace Cinema.WPF.Model
{
    public interface ICinemaService
    {
        bool IsUserLoggedIn { get; }
        Task<bool> LoginAsync(string name, string password);
        Task<bool> LogoutAsync();
        Task<IEnumerable<MovieDto>> LoadMovies();
        Task<IEnumerable<RoomDto>> LoadRooms();
        Task<Boolean> AddNewShow(ShowDto newShowData);
        Task<IEnumerable<ShowDto>> LoadShows();
    }
}

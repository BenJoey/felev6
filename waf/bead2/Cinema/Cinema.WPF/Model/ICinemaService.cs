using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Persistence;

namespace Cinema.WPF.Model
{
    public interface ICinemaService
    {
        bool IsUserLoggedIn { get; }
        Task<bool> LoginAsync(string name, string password);
        Task<bool> LogoutAsync();
        Task<IEnumerable<Movie>> LoadMovies();
        Task<IEnumerable<Room>> LoadRooms();
    }
}

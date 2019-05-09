using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cinema.Persistence;
using Cinema.Persistence.DTOs;


namespace Cinema.WPF.Model
{
    public class CinemaServices : ICinemaService
    {
        private readonly HttpClient _client;

        private bool _isUserLoggedIn;

        public bool IsUserLoggedIn => _isUserLoggedIn;

        public CinemaServices(string baseAddress)
        {
            _isUserLoggedIn = false;
            _client = new HttpClient { BaseAddress = new Uri(baseAddress) };
        }

        public async Task<bool> LoginAsync(string name, string password)
        {
            HttpResponseMessage res = await _client.GetAsync("api/Account/Login/"+name+"/"+password);

            if (res.IsSuccessStatusCode)
            {
                _isUserLoggedIn = true;
                return true;
            }

            if (res.StatusCode == HttpStatusCode.Unauthorized)
            {
                return false;
            }

            throw new NetworkException("Service returned response" + res.StatusCode);
        }

        public async Task<bool> LogoutAsync()
        {
            HttpResponseMessage res = await _client.GetAsync("api/Account/Logout");

            if (res.IsSuccessStatusCode)
            {
                return true;
            }

            throw new NetworkException("Service returned response" + res.StatusCode);
        }

        public async Task<IEnumerable<Movie>> LoadMovies()
        {
            HttpResponseMessage res = await _client.GetAsync("api/Show/MovieList");

            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadAsAsync<IEnumerable<Movie>>();
            }

            throw new NetworkException("Service returned response: " + res.StatusCode);
        }

        public async Task<IEnumerable<Room>> LoadRooms()
        {
            HttpResponseMessage res = await _client.GetAsync("api/Show/RoomList");

            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadAsAsync<IEnumerable<Room>>();
            }

            throw new NetworkException("Service returned response: " + res.StatusCode);
        }

    }
}

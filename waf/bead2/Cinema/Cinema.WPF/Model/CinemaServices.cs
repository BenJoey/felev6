using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Resources;
using Cinema.Persistence;
using Cinema.Persistence.DTOs;
using Newtonsoft.Json;


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
            try
            {
                HttpResponseMessage res = await _client.GetAsync("api/Account/Login/" + name + "/" + password);

                _isUserLoggedIn = res.IsSuccessStatusCode;
                return res.IsSuccessStatusCode;
            }
            catch
            {
                throw new NetworkException("Service error");
            }
        }

        public async Task<bool> LogoutAsync()
        {
            try
            {
                HttpResponseMessage res = await _client.GetAsync("api/Account/Logout");

                if (res.IsSuccessStatusCode)
                {
                    _isUserLoggedIn = false;
                    return true;
                }

                return false;
            }
            catch
            {
                throw new NetworkException("Service error");
            }
        }

        public async Task<IEnumerable<MovieDto>> LoadMovies()
        {
            using (HttpResponseMessage response = await _client.GetAsync("api/Movie"))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<IEnumerable<MovieDto>>();
                }

                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        public async Task<IEnumerable<RoomDto>> LoadRooms()
        {
            HttpResponseMessage res = await _client.GetAsync("api/Show/RoomList");

            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadAsAsync<IEnumerable<RoomDto>>();
            }

            throw new NetworkException("Service returned response: " + res.StatusCode);
        }

        public async Task<Boolean> SendNewShow(ShowDto newShowData)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Show/", newShowData);

            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<ShowDto>> LoadShows()
        {
            HttpResponseMessage res = await _client.GetAsync("api/Show/ShowList");

            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadAsAsync<IEnumerable<ShowDto>>();
            }

            throw new NetworkException("Service returned response: " + res.StatusCode);
        }

        public async Task<IEnumerable<SeatDto>> LoadSeats(int id)
        {
            HttpResponseMessage res = await _client.GetAsync("api/Reservation/SeatList/"+id);

            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadAsAsync<IEnumerable<SeatDto>>();
            }

            throw new NetworkException("Service returned response: " + res.StatusCode);
        }

        public async Task<Boolean> SendReserve(ReservationDto newData)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Reservation/", newData);

            return response.IsSuccessStatusCode;
        }

        public async Task<Boolean> SendNewMovie(MovieDto newData)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Movie/", newData);

            return response.IsSuccessStatusCode;
        }

    }
}

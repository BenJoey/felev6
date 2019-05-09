using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
            EmployeeDto user = new EmployeeDto() { Username = name, Password = password };
            HttpResponseMessage res = await _client.PostAsJsonAsync("api/Account/Login", user);

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

    }
}

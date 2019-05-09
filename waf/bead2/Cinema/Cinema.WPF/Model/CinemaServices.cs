using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.WPF.Model
{
    public class CinemaServices
    {
        private readonly HttpClient _client;

        public bool IsUserLoggedIn;

        public CinemaServices(string baseAddress)
        {
            IsUserLoggedIn = false;
            _client = new HttpClient { BaseAddress = new Uri(baseAddress) };
        }
    }
}

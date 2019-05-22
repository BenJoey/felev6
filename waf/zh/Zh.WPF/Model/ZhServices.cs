using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Resources;
using Zh.Persistence;
using Zh.Persistence.DTOs;
using Newtonsoft.Json;


namespace Zh.WPF.Model
{
    public class ZhServices
    {
        private readonly HttpClient _client;

        private bool _isUserLoggedIn;

        public bool IsUserLoggedIn => _isUserLoggedIn;

        public ZhServices(string baseAddress)
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

        public async Task<IEnumerable<DataDto>> LoadData()
        {
            using (HttpResponseMessage response = await _client.GetAsync("api/Data"))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<IEnumerable<DataDto>>();
                }

                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        public async Task<Boolean> SendNewData(DataDto newData)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Data/", newData);

            return response.IsSuccessStatusCode;
        }

    }
}

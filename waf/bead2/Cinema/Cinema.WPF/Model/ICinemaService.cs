using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.WPF.Model
{
    interface ICinemaService
    {
        bool IsUserLoggedIn { get; }
        Task<bool> LoginAsync(string name, string password);
    }
}

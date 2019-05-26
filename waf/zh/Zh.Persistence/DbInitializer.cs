using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Zh.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(ZhContext context, UserManager<User> userManager, string posterDirectory="")
        {

            if (context.Datas.Any())
            {
                return;
            }

            context.Datas.Add(new Data() {Name = "Test"});

            if (userManager != null)
            {
                var adminUser = new User()
                {
                    UserName = "admin",
                    FullName = "Dolgozo1",
                    Email = "admin@example.com",
                    PhoneNumber = "+36123456789"
                };
                var adminPassword = "Valami123";

                var result1 = userManager.CreateAsync(adminUser, adminPassword).Result;
            }

            context.SaveChanges();
        }
    }
}

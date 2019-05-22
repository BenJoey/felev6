using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Zh.Persistence
{
    public class ZhContext : IdentityDbContext<User>
    {
        public ZhContext (DbContextOptions<ZhContext> options) : base(options) { }

        public DbSet<Data> Datas { get; set; }
    }
}

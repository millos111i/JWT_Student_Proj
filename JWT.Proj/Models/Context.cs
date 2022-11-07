using JWT.Proj.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace JWT.Proj.Models
{
    public class Context : DbContext, IContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}

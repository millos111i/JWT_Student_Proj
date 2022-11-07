using JWT.Proj.Models.Core;
using Microsoft.EntityFrameworkCore;

namespace JWT.Proj.Models
{
    public interface IContext
    {
        DbSet<User> Users { get; set; }
    }
}

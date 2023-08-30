using Microsoft.EntityFrameworkCore;
using WebApp_CRUD_Core.Model;

namespace WebApp_CRUD_Core.Repository
{
    public class APIDbContext :DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }

    }
}

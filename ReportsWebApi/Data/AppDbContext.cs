using Microsoft.EntityFrameworkCore;
using ReportsWebApi.Models;

namespace ReportsWebApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {          
        }

        public DbSet<DateTimeModel> DateTimeModel { get; set; }
        public DbSet<Response> MdxResponse { get; set; }
    }
}

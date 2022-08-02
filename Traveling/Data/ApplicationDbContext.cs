using Traveling.Controllers;
using Traveling.Models;
using Microsoft.EntityFrameworkCore;

namespace Traveling.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<TravelModel> Travels { get; set; }
    }
}

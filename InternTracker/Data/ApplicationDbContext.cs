using InternTracker.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternTracker.Data
{
    public class ApplicationDbContext:DbContext
    {
       public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
        }
        public DbSet<Intern> Interns { get; set; }
    }
}

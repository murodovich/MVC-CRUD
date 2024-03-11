using Microsoft.EntityFrameworkCore;
using Presentation.Models.Entities;

namespace Presentation.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) 
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}

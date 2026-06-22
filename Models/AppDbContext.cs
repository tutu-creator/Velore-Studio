using Microsoft.EntityFrameworkCore;

namespace Velore_Studio.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Testimonial> Testimonials { get; set; }
    public DbSet<ContactMessage> ContactMessages { get; set; }
}
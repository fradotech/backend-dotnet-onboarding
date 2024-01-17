using Microsoft.EntityFrameworkCore;
using Iam.Models; // Tambahkan ini

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<AppUser> Users { get; set; }
}
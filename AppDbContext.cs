using Microsoft.EntityFrameworkCore;
using Iam.Models; // Tambahkan ini

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<AppUser> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>().ToTable("users");
        modelBuilder.Entity<Role>().ToTable("roles");

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            if (entity == null) continue;

            entity.SetTableName(entity.GetTableName()?.ToSnakeCase());

            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(property.GetColumnName().ToSnakeCase());
            }

            foreach (var key in entity.GetKeys())
            {
                key.SetName(key?.GetName()?.ToSnakeCase());
            }

            foreach (var key in entity.GetForeignKeys())
            {
                key.SetConstraintName(key?.GetConstraintName()?.ToSnakeCase());
            }
        }
    }
}
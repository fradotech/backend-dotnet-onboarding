using Microsoft.EntityFrameworkCore;
using Iam.Models; // Tambahkan ini

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public static void CreateConnection (WebApplication app, ILogger logger)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (dbContext.Database.CanConnect())
        {
            logger.LogInformation("Connected to the database successfully!");
        }
        else
        {
            logger.LogError("Failed to connect to the database!");
        }
    }

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
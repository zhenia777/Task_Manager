using Microsoft.EntityFrameworkCore;
using Storage.Entities;

namespace Storage;

public class TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }

    public DbSet<TaskEntity> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<TaskEntity>(entity =>
        {
            entity.HasOne(t => t.User)
                  .WithMany(u => u.Tasks)
                  .HasForeignKey(t => t.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        builder.Entity<UserEntity>(entity =>
        {
            entity.HasIndex(u => u.UserName)
                  .IsUnique();
            entity.HasIndex(u => u.Email)
                  .IsUnique();
        });


    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var userEntries = ChangeTracker.Entries<UserEntity>();
        foreach (var entry in userEntries)
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
            }
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
                entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
            }
        }

        var taskEntries = ChangeTracker.Entries<TaskEntity>();
        foreach (var entry in taskEntries)
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
            }
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
                entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
using Microsoft.EntityFrameworkCore;
using Storage.Entities;
using Storage.Enum;

namespace Storage;

public static class Seed
{
    public static async Task SeedData(TaskManagerDbContext dbContext)
    {
        if (!await dbContext.Users.AnyAsync())
        {
            var testUser = new UserEntity
            {
                Id = Guid.NewGuid(),
                UserName = "testuser",
                Email = "test@test.com",
                PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("Password1"),
            };

            await dbContext.Users.AddAsync(testUser);

            var testTasks = new List<TaskEntity>
            {
                new TaskEntity
                {
                    Id = Guid.NewGuid(),
                    Title = "Buy groceries",
                    Description = "Milk, bread, cheese, and eggs.",
                    DueDate = DateTimeOffset.UtcNow.AddDays(1),
                    Status = (int)TasksStatus.Pending,
                    Priority = (int)TasksPriority.Low,
                    UserId = testUser.Id
                },
                new TaskEntity
                {
                    Id = Guid.NewGuid(),
                    Title = "Do laundry",
                    Description = "Wash and fold clothes.",
                    DueDate = DateTimeOffset.UtcNow.AddDays(1),
                    Status = (int)TasksStatus.Pending,
                    Priority = (int)TasksPriority.Low,
                    UserId = testUser.Id
                },
                new TaskEntity
                {
                    Id = Guid.NewGuid(),
                    Title = "Finish the test project",
                    Description = "Complete the .NET task management system.",
                    DueDate = DateTimeOffset.UtcNow.AddDays(3),
                    Status = (int)TasksStatus.Pending,
                    Priority = (int)TasksPriority.Low,
                    UserId = testUser.Id
                }
            };

            await dbContext.Tasks.AddRangeAsync(testTasks);
            await dbContext.SaveChangesAsync();
        }
    }
}

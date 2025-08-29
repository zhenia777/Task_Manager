using System.ComponentModel.DataAnnotations;

namespace Storage.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    [Required]
    public required string UserName { get; set; }
    [Required]
    public required string Email { get; set; }
    [Required]
    public required string PasswordHash { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public ICollection<TaskEntity>? Tasks { get; set; }
}


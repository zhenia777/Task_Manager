using Storage.Enum;
using System.ComponentModel.DataAnnotations;

namespace Storage.Entities;

public class TaskEntity
{
    public Guid Id { get; set; }
    [Required]
    public required string Title { get; set; }
    
    public string? Description { get; set; }
    
    public DateTimeOffset? DueDate { get; set; }
    
    public TasksStatus Status { get; set; }
    
    public TasksPriority Priority { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public DateTimeOffset UpdatedAt { get; set; }

    public Guid UserId { get; set; }

    public UserEntity? User { get; set; }
}

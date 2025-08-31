using API.Enums;

namespace API.Dtos.Tasks;

public class UpdateTaskDto
{
    public Guid Id { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public DateTimeOffset? DueDate { get; set; }

    public TasksStatus Status { get; set; }

    public TasksPriority Priority { get; set; }
}

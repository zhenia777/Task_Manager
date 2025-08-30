namespace Domain.Queries;

public record class PaginationQuery
{
    public int Page { get; set; } = 1;
    public int ItemPerPage { get; set; } = 5;
}

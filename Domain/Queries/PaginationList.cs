namespace Domain.Queries;

public class PaginationList<T>
{
    public required IEnumerable<T> List { get; set; }
    public int TotalSize { get; set; }
}

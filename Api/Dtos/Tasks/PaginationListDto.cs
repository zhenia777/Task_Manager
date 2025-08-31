namespace API.Dtos.Tasks;

public class PaginationListDto<T>
{
    public required IEnumerable<T> List { get; set; }
    public int TotalSize { get; set; }
}

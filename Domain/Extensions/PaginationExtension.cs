using Domain.Queries;
using Microsoft.EntityFrameworkCore;

namespace Domain.Extensions;

public static class PaginationExtensions
{
    public static PaginationList<T> ToPagination<T>(this IQueryable<T> list, PaginationQuery query)
    {
        return new PaginationList<T>
        {
            TotalSize = list.Count(),
            List = list.Skip((query.Page - 1) * query.ItemPerPage).Take(query.ItemPerPage)
        };
    }

    public static async Task<PaginationList<T>> ToPagination<T>(this IQueryable<T> list, PaginationQuery query,
                                                            CancellationToken cancellationToken)
    {
        return new PaginationList<T>
        {
            TotalSize = await list.CountAsync(cancellationToken),
            List = await list.Skip((query.Page - 1) * query.ItemPerPage).Take(query.ItemPerPage).ToListAsync(cancellationToken)
        };
    }
}
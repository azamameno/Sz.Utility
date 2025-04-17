using Microsoft.EntityFrameworkCore;

namespace Sz.Utility;

public class PaginatedList<T>(List<T> items, int count) {
    public List<T> Items { get; } = items;
    public int Count { get; } = count;

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize, CancellationToken cancellationToken) {
        var count = await source.CountAsync(cancellationToken);
        var items = await source.Paging(pageIndex * pageSize, pageSize).ToListAsync(cancellationToken);

        return new PaginatedList<T>(items, count);
    }
}

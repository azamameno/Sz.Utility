using System.Linq.Expressions;

namespace System.Linq;

public static class LinqExtension {
    public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool when,
        Expression<Func<TSource, bool>> predicateTrue,
        Expression<Func<TSource, bool>>? predicateFalse = null) {
        if (when) return source.Where(predicateTrue);
        return predicateFalse != null ? source.Where(predicateFalse) : source;
    }

    public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source,
        Expression<Func<TSource, bool>> when,
        Expression<Func<TSource, bool>> predicateTrue) {
        var expression = Expression.Lambda<Func<TSource, bool>>(Expression.Or(Expression.And(when, predicateTrue), Expression.Not(when)));
        return source.Where(expression);
    }

    public static IQueryable<TSource> WhereFunc<TSource>(this IQueryable<TSource> source, bool when,
        Func<IQueryable<TSource>, IQueryable<TSource>> funcTrue,
        Func<IQueryable<TSource>, IQueryable<TSource>>? funcFalse = null) {
        if (when) {
            return funcTrue.Invoke(source);
        }

        return funcFalse != null ? funcFalse.Invoke(source) : source;
    }

    public static IQueryable<TSource> Paging<TSource>(this IQueryable<TSource> source, int skip, int take) {
        return source.Paging(true, skip, take);
    }

    public static IQueryable<TSource> Paging<TSource>(this IQueryable<TSource> source, bool when, int skip, int take) {
        return when ? source.Skip(skip).Take(take) : source;
    }
}

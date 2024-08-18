public static class ExtensionsAny
{
    public static bool HasNoOne<TSource>(this IReadOnlyCollection<TSource> collection) => collection.Count == 0;
    public static bool HasAny<TSource>(this IReadOnlyCollection<TSource> collection) => collection.Count > 0;
    public static bool HasOne<TSource>(this IReadOnlyCollection<TSource> collection) => collection.Count == 1;
    public static bool HasMany<TSource>(this IReadOnlyCollection<TSource> collection) => collection.Count > 1;
}
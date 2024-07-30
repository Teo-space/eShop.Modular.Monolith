public static class ExtensionsAny
{
    //public static bool NotEmpty<TSource>(this ICollection<TSource> collection) => collection.Count > 0;
    public static bool NotEmpty<TSource>(this IReadOnlyCollection<TSource> collection) => collection.Count > 0;

    //public static bool Empty<TSource>(this ICollection<TSource> collection) => collection.Count == 0;
    public static bool Empty<TSource>(this IReadOnlyCollection<TSource> collection) => collection.Count == 0;

}
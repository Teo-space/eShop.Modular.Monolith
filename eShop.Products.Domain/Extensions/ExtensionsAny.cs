public static class ExtensionsAny
{
    public static bool Any<TSource>(this ICollection<TSource> collection) => collection.Count > 0;
    //public static bool Any<TSource>(this IReadOnlyCollection<TSource> collection) => collection.Count > 0;

}
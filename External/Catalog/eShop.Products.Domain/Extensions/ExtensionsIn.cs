public static class ExtensionsIn
{
    public static bool In<T>(this T element, params T[] elements) => elements.Contains(element);
    public static bool NotIn<T>(this T element, params T[] elements) => !elements.Contains(element);

}

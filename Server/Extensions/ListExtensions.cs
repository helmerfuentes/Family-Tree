namespace Server.Extensions
{
    public static class ListExtensions
    {
        public static bool HasValues<T>(this IEnumerable<T> items)
            => items?.Any() ?? false;
    }
}

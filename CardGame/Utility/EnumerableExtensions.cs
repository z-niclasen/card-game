namespace CardGame.Utility;

// Source - https://stackoverflow.com/a/5807238
// Posted by LukeH, modified by community. See post 'Timeline' for change history
// Retrieved 2026-08-26, License - CC BY-SA 4.0

public static class EnumerableExtensions
{
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(rng);

        return source.ShuffleIterator(rng);
    }

    private static IEnumerable<T> ShuffleIterator<T>(
        this IEnumerable<T> source, Random rng)
    {
        var buffer = source.ToList();
        for (int i = 0; i < buffer.Count; i++)
        {
            int j = rng.Next(i, buffer.Count);
            yield return buffer[j];

            buffer[j] = buffer[i];
        }
    }
}

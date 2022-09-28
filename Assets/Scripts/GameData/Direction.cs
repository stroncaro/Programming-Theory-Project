public static class Direction
{
    public enum World { NORTH, EAST, SOUTH, WEST }
    public enum Relative { FORWARD, RIGHT, BACK, LEFT }

    public static int Count = 4;

    private static int _Wrap(int n, int minInclusive, int maxExclusive)
    {
        bool condition = n >= minInclusive && n < maxExclusive;

        if (!condition)
        {
            int min = minInclusive;
            int max = maxExclusive;

            n = (n - min) % (max - min) + min;
            if (n < min) n += min;
        }

        return n;
    }
    private static int _Wrap(int n, int maxExclusive) => _Wrap(n, 0, maxExclusive);

    public static World toWorld(this Relative dir, World facingDirection)
    {
        int a = (int)dir;
        int b = (int)facingDirection;
        int val = _Wrap(a + b, Count);
        return (World)val;
    }
        
    public static Relative toRelative(this World dir, World facingDirection)
    {
        int a = (int)dir;
        int b = (int)facingDirection;
        int val = _Wrap(a - b, Count);
        return (Relative)val;
    }
}

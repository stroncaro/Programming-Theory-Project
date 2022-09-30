using System.Collections.Generic;
using UnityEngine;

public static class Direction
{
    public enum World { NORTH, EAST, SOUTH, WEST }
    public enum Relative { FORWARD, RIGHT, BACK, LEFT }

    public static readonly int Count = 4;

    public static Dictionary<World, Vector2> WorldToVector2 = new Dictionary<World, Vector2>
    {
        { World.NORTH, Vector2.up },
        { World.EAST, Vector2.right },
        { World.SOUTH, Vector2.down },
        { World.WEST, Vector2.left }
    };

    private static int _Wrap(int n, int minInclusive, int maxExclusive)
    {
        //wrap values between min and max, overflow if value goes above or below
        bool condition = n >= minInclusive && n < maxExclusive;

        if (!condition)
        {
            int min = minInclusive;
            int max = maxExclusive;
            n = (n - min) % (max - min) + min;
            if (n < min) n += (max - min);
        }

        return n;
    }
    private static int _Wrap(int n, int maxExclusive) => _Wrap(n, 0, maxExclusive);

    public static World toWorld(this Relative dir, World facingDirection)
    {
        int a = (int)dir;
        int b = (int)facingDirection;
        int newDir = _Wrap(a + b, Count);
        return (World)newDir;
    }
        
    public static Relative toRelative(this World dir, World facingDirection)
    {
        int a = (int)dir;
        int b = (int)facingDirection;
        int newDir = _Wrap(a - b, Count);
        return (Relative)newDir;
    }

    public static World Rotate(this World origin, int steps)
    {
        int a = (int)origin;
        int newDir = _Wrap(a + steps, Count);
        return (World)newDir;
    }

    public static Relative Rotate(this Relative origin, int steps)
    {
        int a = (int)origin;
        int newDir = _Wrap(a + steps, Count);
        return (Relative)newDir;
    }
}

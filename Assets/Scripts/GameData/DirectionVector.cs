using System.Collections.Generic;
using UnityEngine;

public static class DirectionVector
{
    private static Dictionary<Direction.World, Vector2> _WorldToVector2 = new Dictionary<Direction.World, Vector2>
    {
        { Direction.World.NORTH, Vector2.up },
        { Direction.World.EAST, Vector2.right },
        { Direction.World.SOUTH, Vector2.down },
        { Direction.World.WEST, Vector2.left }
    };

    public static Vector2 toVector2(this Direction.World dir) => _WorldToVector2[dir];

    public static Vector2 north { get => Direction.World.NORTH.toVector2(); }
    public static Vector2 east { get => Direction.World.EAST.toVector2(); }
    public static Vector2 south { get => Direction.World.SOUTH.toVector2(); }
    public static Vector2 west { get => Direction.World.WEST.toVector2(); }
}

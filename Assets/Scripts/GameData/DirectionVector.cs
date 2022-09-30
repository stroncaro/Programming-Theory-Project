using UnityEngine;

public static class DirectionVector
{
    public static Vector2Int north { get => Vector2Int.up; }
    public static Vector2Int east { get => Vector2Int.right; }
    public static Vector2Int south { get => Vector2Int.down; }
    public static Vector2Int west { get => Vector2Int.left; }
}

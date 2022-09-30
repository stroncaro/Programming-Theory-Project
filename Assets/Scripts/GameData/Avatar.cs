using UnityEngine;

public class Avatar : Entity
{
    protected override string SetType() => "avatar";

    private Direction.World _facingDir;
    public Direction.World facingDirection { get => _facingDir; }

    public Direction.World forwardDirection { get => _facingDir; }
    public Direction.World backDirection { get => Direction.Relative.BACK.toWorld(_facingDir); }
    public Direction.World rightDirection { get => Direction.Relative.RIGHT.toWorld(_facingDir); }
    public Direction.World leftDirection { get => Direction.Relative.LEFT.toWorld(_facingDir); }
    public Vector2 forward { get => Direction.WorldToVector2[forwardDirection]; }
    public Vector2 right { get => Direction.WorldToVector2[rightDirection]; }
    public Vector2 back { get => Direction.WorldToVector2[backDirection]; }
    public Vector2 left { get => Direction.WorldToVector2[leftDirection]; }

    public bool IsFacing(Direction.World dir) => dir == _facingDir;
    public void RotateClockwise(int times = 1) => _facingDir = _facingDir.Rotate(times);
    public void RotateCounterclockwise(int times = 1) => _facingDir = _facingDir.Rotate(-times);

    public void Translate(Direction.World dir, int steps = 1)
    {
        Vector2 movement = Direction.WorldToVector2[dir] * steps;
        Translate(movement);
    }

    public void Translate(Direction.Relative dir, int steps = 1)
    {
        Direction.World absDir = dir.toWorld(_facingDir);
        Translate(absDir, steps);
    }

    public void Translate(Vector2 movement)
    {
        if (movement.x != 0 && movement.y != 0)
        {
            Debug.LogWarning($"{name} invoked Translate({movement}). Separating in two different Vectors");
            Vector2[] movements = new Vector2[]
            {
                new Vector2(movement.x, 0),
                new Vector2(movement.y, 0)
            };

            Translate(movements);
        }
        else
            SetPosition(position + movement);
    }

    public void Translate(Vector2[] movements)
    {
        foreach (Vector2 movement in movements)
            Translate(movement);
    }

    public void MoveForward(int steps = 1) => Translate(forward * steps);
    public void MoveRight(int steps = 1) => Translate(right * steps);
    public void MoveLeft(int steps = 1) => Translate(left * steps);
    public void MoveBack(int steps = 1) => Translate(back * steps);
    public void MoveNorth(int steps = 1) => Translate(Direction.WorldToVector2[Direction.World.NORTH] * steps);
    public void MoveEast(int steps = 1) => Translate(Direction.World.EAST, steps);
    public void MoveSouth(int steps = 1) => Translate(Direction.World.SOUTH, steps);
    public void MoveWest(int steps = 1) => Translate(Direction.World.WEST, steps);

    public Avatar(string name, int x, int y, Direction.World facingDirection) : base(name, x, y)
    {
        _facingDir = facingDirection;
    }
}

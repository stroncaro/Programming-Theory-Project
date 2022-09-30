using UnityEngine;

public class Avatar : Entity
{
    protected override string SetType() => "avatar";

    private Direction.World _facingDir;
    public Direction.World facingDirection { get => _facingDir; }

    public Direction.World GetDirection(Direction.Relative direction) => direction.toWorld(_facingDir);
    public Vector2 forward { get => _facingDir.toVector2(); }
    public Vector2 right { get => GetDirection(Direction.Relative.RIGHT).toVector2(); }
    public Vector2 back { get => GetDirection(Direction.Relative.BACK).toVector2(); }
    public Vector2 left { get => GetDirection(Direction.Relative.LEFT).toVector2(); }

    public bool IsFacing(Direction.World dir) => dir == _facingDir;
    public void RotateClockwise(int times = 1) => _facingDir = _facingDir.Rotate(times);
    public void RotateCounterclockwise(int times = 1) => _facingDir = _facingDir.Rotate(-times);

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
    public void MoveNorth(int steps = 1) => Translate(DirectionVector.north * steps);
    public void MoveEast(int steps = 1) => Translate(DirectionVector.east * steps);
    public void MoveSouth(int steps = 1) => Translate(DirectionVector.south * steps);
    public void MoveWest(int steps = 1) => Translate(DirectionVector.west * steps);

    public Avatar(string name, int x, int y, Direction.World facingDirection) : base(name, x, y)
    {
        _facingDir = facingDirection;
    }
}

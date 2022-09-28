using UnityEngine;

public class Avatar : Entity
{
    private Direction.World _facingDir;
    public Direction.World facingDirection { get => _facingDir; }

    public Direction.World forward { get => _facingDir; }
    public Direction.World back { get => Direction.Relative.BACK.toWorld(_facingDir); }
    public Direction.World right { get => Direction.Relative.RIGHT.toWorld(_facingDir); }
    public Direction.World left { get => Direction.Relative.LEFT.toWorld(_facingDir); }
    public bool IsFacing(Direction.World dir) => dir == _facingDir;
    public void RotateClockwise(int times = 1) => _facingDir = _facingDir.Rotate(times);
    public void RotateCounterclockwise(int times = 1) => _facingDir = _facingDir.Rotate(-times);

    public void Move(Direction.World dir, int steps = 1)
    {
        Vector2 movement = Direction.WorldToVector2[dir] * steps;
        int newX = _x + (int)movement.x;
        int newY = _y + (int)movement.y;

        //check board bounds
        int boardX = GameData.GetBoard().rows;
        int boardY = GameData.GetBoard().files;
        newX = newX >= boardX ? boardX - 1 : newX;
        newX = newX < 0 ? 0 : newX;
        newY = newY >= boardY ? boardY -1 : newY;
        newY = newY < 0 ? 0 : newY;

        UnregisterFromTile();
        _x = newX;
        _y = newY;
        RegisterInTile();
    }

    public void Move(Direction.Relative dir, int steps = 1)
    {
        Direction.World absDir = dir.toWorld(_facingDir);
        Move(absDir, steps);
    }

    public void MoveForward(int steps = 1) => Move(Direction.Relative.FORWARD, steps);
    public void MoveRight(int steps = 1) => Move(Direction.Relative.RIGHT, steps);
    public void MoveLeft(int steps = 1) => Move(Direction.Relative.LEFT, steps);
    public void MoveBack(int steps = 1) => Move(Direction.Relative.BACK, steps);
    public void MoveNorth(int steps = 1) => Move(Direction.World.NORTH, steps);
    public void MoveEast(int steps = 1) => Move(Direction.World.EAST, steps);
    public void MoveSouth(int steps = 1) => Move(Direction.World.SOUTH, steps);
    public void MoveWest(int steps = 1) => Move(Direction.World.WEST, steps);

    public Avatar(string name, int x, int y, Direction.World facingDirection) : base(name, x, y)
    {
        _facingDir = facingDirection;
    }

    protected override string SetType() => "avatar";
}

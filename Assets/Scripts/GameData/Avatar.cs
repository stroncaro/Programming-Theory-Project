using System;

public class Avatar : Entity
{
    private Board.Direction _direction;
    public Board.Direction direction { get => _direction; }
    public Board.Direction forward { get => _direction; }
    public Board.Direction back { get => Board.Rotation(_direction, 2); }
    public Board.Direction right { get => Board.Rotation(_direction, 1); }
    public Board.Direction left { get => Board.Rotation(_direction, -1); }
    public bool IsFacing(Board.Direction dir) => dir == _direction;
    public void RotateClockwise(int times = 1) => _direction = Board.Rotation(_direction, times);
    public void RotateCounterclockwise(int times = 1) => _direction = Board.Rotation(_direction, -times);

    public Avatar(string name, int x, int y, Board.Direction direction) : base(name, x, y)
    {
        _direction = direction;
    }

    protected override string SetType() => "avatar";
}

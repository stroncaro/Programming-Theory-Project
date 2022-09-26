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

    public enum Direction { FORWARD, RIGHT, LEFT, BACK }

    public void Move(Avatar.Direction dir, int steps = 1)
    {
        Board.Direction boardDir;
        switch (dir)
        {
            case Direction.FORWARD:
                boardDir = forward;
                break;
            case Direction.RIGHT:
                boardDir = right;
                break;
            case Direction.LEFT:
                boardDir = left;
                break;
            case Direction.BACK:
                boardDir = back;
                break;
            default:
                return;
        }
        int newX = _x;
        int newY = _y;
        switch (boardDir)
        {
            case Board.Direction.UP:
                newY += steps;
                break;
            case Board.Direction.RIGHT:
                newX += steps;
                break;
            case Board.Direction.DOWN:
                newY -= steps;
                break;
            case Board.Direction.LEFT:
                newX -= steps;
                break;
            default:
                return;
        }

        //TODO check board bounds here

        UnregisterFromTile();
        _x = newX;
        _y = newY;
        RegisterInTile();
    }

    public void MoveForward(int steps = 1) => Move(Avatar.Direction.FORWARD, steps);
    public void MoveRight(int steps = 1) => Move(Avatar.Direction.RIGHT, steps);
    public void MoveLeft(int steps = 1) => Move(Avatar.Direction.LEFT, steps);
    public void MoveBack(int steps = 1) => Move(Avatar.Direction.BACK, steps);

    public Avatar(string name, int x, int y, Board.Direction direction) : base(name, x, y)
    {
        _direction = direction;
    }

    protected override string SetType() => "avatar";
}

public class Avatar : Entity
{
    private Board.Direction _direction;
    public Board.Direction direction { get => _direction; }

    public Avatar(string name, int x, int y, Board.Direction direction) : base(name, x, y)
    {
        _direction = direction;
    }

    protected override string SetType() => "avatar";
}

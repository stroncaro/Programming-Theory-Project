public class Avatar : Entity
{
    private Board.Direction _facing;
    public Board.Direction facing { get => _facing; }

    public Avatar(string name, int x, int y, Board.Direction facing) : base(name, x, y)
    {
        _facing = facing;
    }

    protected override string SetType() => "avatar";
}

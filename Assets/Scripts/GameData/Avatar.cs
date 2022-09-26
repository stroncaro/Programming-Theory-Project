public class Avatar : Entity
{
    private BoardDirection _facing;
    public BoardDirection facing { get => _facing; }

    public Avatar(string name, int x, int y, BoardDirection facing) : base(name, x, y)
    {
        _facing = facing;
    }

    protected override string SetType() => "avatar";
}

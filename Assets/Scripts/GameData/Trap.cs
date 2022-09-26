public class Trap : Entity
{
    public Trap(string name, int x, int y) : base(name, x, y) { }

    protected override string SetType() => "trap";
}

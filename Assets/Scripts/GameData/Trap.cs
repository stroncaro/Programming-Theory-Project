using UnityEngine;

public class Trap : Entity
{
    public Trap(string name, int x, int y) : base(name, x, y) { }

    protected override string SetType() => "trap";

    public override void onProc()
    {
        Debug.Log("Trap activated!");
        Kill();
    }
}

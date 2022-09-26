using UnityEngine;

public class Entity
{
    private string _name;
    public string name { get => _name; }

    private int _x, _y;
    public int x { get => _x; }
    public int y { get => _y; }
    public Vector2 position { get => new Vector2(_x, _y); }

    public Entity(string name, int x = 0, int y = 0)
    {
        _name = name;
        _x = x;
        _y = y;
    }
}

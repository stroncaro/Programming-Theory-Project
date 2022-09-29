using UnityEngine;

public abstract class Entity
{
    private static class EntityIDGenerator
    {
        private static int _counter = 0;
        public static int GetNewId()
        {
            var val = _counter;
            _counter++;
            return val;
        }
    }

    protected readonly int _id;
    public int id { get => _id; }

    protected readonly string _name;
    public string name { get => _name; }

    protected readonly string _type;
    public string type { get => _type; }
    protected abstract string SetType();

    protected int _x, _y;
    public int x { get => _x; }
    public int y { get => _y; }
    public Vector2 position { get => new Vector2(_x, _y); }
    public void SetPosition(Vector2 newPosition)
    {
        var board = GameData.GetBoard();
        if (!board.IsInBoard(newPosition))
            newPosition = board.ClampToBoard(newPosition);

        if (position != newPosition)
        {
            UnregisterFromTile();
            _x = (int)newPosition.x;
            _y = (int)newPosition.y;
            RegisterInTile();
        }
    }

    public Vector2 northVector { get => Direction.WorldToVector2[Direction.World.NORTH]; }
    public Vector2 eastVector { get => Direction.WorldToVector2[Direction.World.EAST]; }
    public Vector2 southVector { get => Direction.WorldToVector2[Direction.World.SOUTH]; }
    public Vector2 westVector { get => Direction.WorldToVector2[Direction.World.WEST]; }

    public Entity(string name, int x = 0, int y = 0)
    {
        _id = EntityIDGenerator.GetNewId();
        _name = name;
        _x = x;
        _y = y;
        _type = SetType();

        RegisterInTile();
        RegisterEntity();
    }

    protected void RegisterInTile(int x, int y) => GameData.GetBoard().GetTile(x, y).RegisterEntity(_id);
    protected void RegisterInTile() => RegisterInTile(_x, _y);
    protected void UnregisterFromTile(int x, int y) => GameData.GetBoard().GetTile(x, y).UnregisterEntity(_id);
    protected void UnregisterFromTile() => UnregisterFromTile(_x, _y);

    protected void RegisterEntity() => GameData.AddEntity(this);
    protected void UnregisterEntity() => GameData.RemoveEntity(this);

    public void Kill()
    {
        UnregisterFromTile();
        UnregisterEntity();
    }

    public virtual void onProc() { }
}

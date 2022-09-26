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

    protected int _x, _y;
    public int x { get => _x; }
    public int y { get => _y; }
    public Vector2 position { get => new Vector2(_x, _y); }

    public Entity(string name, int x = 0, int y = 0)
    {
        _id = EntityIDGenerator.GetNewId();
        _name = name;
        _x = x;
        _y = y;
        _type = SetType();

        GameData.Instance.board.tiles[_x, _y].entityIds.Add(_id);
        GameData.Instance.entities.Add(this);
    }

    protected abstract string SetType();

    public void Kill()
    {
        GameData.Instance.board.tiles[_x, _y].entityIds.Remove(_id);
        GameData.Instance.entities.Remove(this);
    }
}

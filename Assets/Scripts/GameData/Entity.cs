using UnityEngine;

public class Entity
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

        GameData.Instance.board.tiles[_x, _y].entityIds.Add(_id);
        GameData.Instance.entities.Add(this);
    }

    public void Kill()
    {
        GameData.Instance.board.tiles[_x, _y].entityIds.Remove(_id);
        GameData.Instance.entities.Remove(this);
    }
}

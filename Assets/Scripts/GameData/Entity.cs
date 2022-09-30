using System;
using UnityEngine;

public abstract class Entity
{
    public EventHandler EntityDataUpdated;
    public virtual void OnEntityDataUpdated() => EntityDataUpdated?.Invoke(this, null);

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

    private readonly int _id;
    public int id { get => _id; }

    private readonly string _name;
    public string name { get => _name; }

    private readonly string _type;
    public string type { get => _type; }
    protected abstract string SetType();

    private int _x, _y;
    public int x { get => _x; }
    public int y { get => _y; }
    public Vector2 position { get => new Vector2(_x, _y); }
    protected void SetPosition(Vector2 newPosition)
    {
        newPosition = GameData.GetBoard().ClampToBoard(newPosition);

        if (position != newPosition)
        {
            UnregisterFromTile();
            _x = (int)newPosition.x;
            _y = (int)newPosition.y;
            RegisterInTile();
        }
    }

    public Entity(string name, int x = 0, int y = 0)
    {
        _id = EntityIDGenerator.GetNewId();
        _name = name;
        _x = x;
        _y = y;
        _type = SetType();

        RegisterEntity();
        RegisterInTile();
    }

    private void RegisterInTile(int x, int y) => GameData.GetBoard().GetTile(x, y).RegisterEntity(_id);
    private void RegisterInTile(Vector2 tilePos) => RegisterInTile((int)tilePos.x, (int)tilePos.y);
    private void RegisterInTile() => RegisterInTile(_x, _y);
    private void UnregisterFromTile(int x, int y) => GameData.GetBoard().GetTile(x, y).UnregisterEntity(_id);
    private void UnregisterFromTile(Vector2 tilePos) => UnregisterFromTile((int)tilePos.x, (int)tilePos.y);
    private void UnregisterFromTile() => UnregisterFromTile(_x, _y);

    private void RegisterEntity() => GameData.AddEntity(this);
    private void UnregisterEntity() => GameData.RemoveEntity(this);

    public void Kill()
    {
        UnregisterFromTile();
        UnregisterEntity();
    }

    public virtual void onProc() { }
}

using System.Collections.Generic;

public class Tile
{
    private bool _active;   //flag if the tile is in the active game area
    public bool isActive { get => _active; set => _active = value; }
    public void SetActive(bool status) { _active = status; }
    public void Activate() { if (!_active) { _active = true; } }
    public void Deactivate() { if (_active) { _active = false; } }

    private List<int> _entityIds;
    public void RegisterEntity(int id) => _entityIds.Add(id);
    public void UnregisterEntity(int id) => _entityIds.Remove(id);

    public bool hasEntities { get => _entityIds.Count > 0; }
    public bool ContainsEntity(int id) => _entityIds.Contains(id);
    public Entity GetEntity(int i = 0) => GameData.GetEntityById(_entityIds[i]);
    public Entity[] GetEntities()
    {
        Entity[] entities = new Entity[_entityIds.Count];
        for (int i = 0; i < _entityIds.Count; i++) entities[i] = GetEntity(i);
        return entities;
    }

    public Tile(bool active = true)
    {
        _active = active;
        _entityIds = new List<int>();
    }
}

using System;
using System.Collections.Generic;

public class Tile
{
    //event to tell suscriptors tile data has been updated
    public event EventHandler TileDataUpdated;
    protected virtual void OnTileDataUpdated() => TileDataUpdated?.Invoke(this, null);

    //when entity updates, tell suscriptors tile data has updated
    protected virtual void OnEntityDataUpdated(object source, EventArgs args) => OnTileDataUpdated();


    private bool _active;
    public bool isActive
    {
        get => _active;
        set
        {
            _active = value;
            OnTileDataUpdated();
        }
    }    
    public void SetActive(bool status) { isActive = status; }
    public void Activate() { if (!isActive) { isActive = true; } }
    public void Deactivate() { if (isActive) { isActive = false; } }

    private List<int> _entityIds;
    public void RegisterEntity(int id)
    {
        //proc entities that are already on tile
        if (hasEntities)
            foreach (Entity entity in GetEntities())
                entity.onProc();

        //add entity to tile
        _entityIds.Add(id);

        //subscribe to get updates on entity
        GameData.GetEntityById(id).EntityDataUpdated += OnEntityDataUpdated;
        
        //update suscribers
        OnTileDataUpdated();
    }
    public void UnregisterEntity(int id)
    {
        _entityIds.Remove(id);
        
        //unsuscribe from entity
        GameData.GetEntityById(id).EntityDataUpdated -= OnEntityDataUpdated; 
        
        OnTileDataUpdated();
    }

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

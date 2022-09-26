public class Tile
{
    private bool _active;   //flag if the tile is in the active game area
    public bool isActive { get => _active; set => _active = value; }
    public void SetActive(bool status) { _active = status; }
    public void Activate() { if (!_active) { _active = true; } }
    public void Deactivate() { if (_active) { _active = false; } }

    //TODO:  make a list to hold the pieces that are in the tile

    public Tile(bool active = true)
    {
        _active = active;
    }
}

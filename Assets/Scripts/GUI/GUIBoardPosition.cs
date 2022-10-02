using System;
using UnityEngine;
using TMPro;

public class GUIBoardPosition : MonoBehaviour
{
    private bool _initialized = false;
    
    private Vector2Int _tileCoordinates;
    public Vector2Int TileCoordinates { get => _tileCoordinates; }
    public int x { get => _tileCoordinates.x; }
    public int y { get => _tileCoordinates.y; }
    private Tile _tile;
    public Tile Tile { get => _tile; }

    public void SetTileCoordinates(int x, int y)
    {
        if (_initialized)
        {
            Debug.LogWarning($"{name} already initialized. Can't set tile coordinates");
            return;
        }

        _tileCoordinates = new Vector2Int(x, y);
    }

    private BoxCollider2D _collider;
    private TextMeshPro _tmpro;
    private RectTransform _rectTransform;

    public EventHandler MouseEnterTile;
    protected virtual void OnMouseEnterTile() => MouseEnterTile?.Invoke(this, null);

    public EventHandler MouseExitTile;
    protected virtual void OnMouseExitTile() => MouseExitTile?.Invoke(this, null);

    public EventHandler BoardPositionDataUpdated;
    protected virtual void OnBoardPositionDataUpdated() => BoardPositionDataUpdated?.Invoke(this, null);

    private void UpdateTileText()
    {
        string text = GUIManager.EmptySymbol;
        if (_tile.hasEntities)
        {
            var thisEntity = _tile.GetEntity();
            switch (thisEntity.type)
            {
                case "trap":
                    text = GUIManager.TrapSymbol;
                    break;
                case "avatar":
                    text = GUIManager.AvatarDirectionSymbol[(thisEntity as Avatar).facingDirection];
                    break;
            }
        }
        _tmpro.text = text;
    }
    private void UpdateTileColor()
    {
        _tmpro.color = _tile.isActive ? GUIManager.ActiveTileColor : GUIManager.InactiveTileColor;
    }

    private void UpdateTile()
    {
        UpdateTileColor();
        UpdateTileText();
        OnBoardPositionDataUpdated();
    }

    public void OnTileDataUpdated(object source, EventArgs args) => UpdateTile();

    private void Initialize()
    {
        if (_tileCoordinates == null)
        {
            Debug.LogWarning($"{gameObject.name}.Initialize() => tile coordinates not set");
            return;
        }

        _collider = gameObject.AddComponent<BoxCollider2D>();
        _collider.size = new Vector2(0.5f, 0.5f);
        _collider.offset = Vector2.zero;

        _tmpro = gameObject.AddComponent<TextMeshPro>();
        _tmpro.fontSize = GUIManager.FontSize;
        _tmpro.fontStyle = GUIManager.FontStyle;
        _tmpro.alignment = GUIManager.FontAlignment;
        _tmpro.enableWordWrapping = GUIManager.EnableTextWrapping;

        _rectTransform = gameObject.GetComponent<RectTransform>();
        _rectTransform.sizeDelta = new Vector2(GUIManager.CellWidth, GUIManager.CellWidth);

        transform.position = new Vector2(x * GUIManager.CellWidth, y * GUIManager.CellWidth);
        _tile = GameData.GetBoard().GetTile(x, y);
        _tile.TileDataUpdated += OnTileDataUpdated;

        _initialized = true;
    }

    private void Start()
    {
        Initialize();
        if (_initialized) UpdateTile();
    }

    private void OnMouseEnter() => OnMouseEnterTile();
    private void OnMouseExit() => OnMouseExitTile();
}

using System;
using UnityEngine;
using TMPro;

public class GUIBoardPosition : MonoBehaviour
{
    private bool _initialized = false;
    
    private Vector2Int _tileCoordinates;
    private int _tileX { get => _tileCoordinates.x; }
    private int _tileY { get => _tileCoordinates.y; }
    private Tile _tile;

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

    public EventHandler MouseOverTile;
    protected virtual void OnMouseOverTile() => MouseOverTile?.Invoke(this, null);

    public bool mouseOver = false;

    public void UpdateTileText()
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
    public void UpdateTileColor()
    {
        _tmpro.color = _tile.isActive ? GUIManager.ActiveTileColor : GUIManager.InactiveTileColor;
    }

    public void UpdateTile()
    {
        UpdateTileColor();
        UpdateTileText();
    }

    public void OnDataUpdated(object source, EventArgs args) => UpdateTile();

    public void Initialize()
    {
        if (_initialized)
        {
            Debug.LogWarning($"{gameObject.name}.Initialize() => Obj already initialized. Ignoring request");
            return;
        }

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

        transform.position = new Vector2(_tileX * GUIManager.CellWidth, _tileY * GUIManager.CellWidth);
        _tile = GameData.GetBoard().GetTile(_tileX, _tileY);
        _tile.TileDataUpdated += OnDataUpdated;
    }

    public void Start()
    {
        Initialize();
        if (_initialized) UpdateTile();
    }

    private void OnMouseEnter() => mouseOver = true;
    private void OnMouseExit() => mouseOver = false;
}

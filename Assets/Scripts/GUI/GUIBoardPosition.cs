using System;
using UnityEngine;
using TMPro;

public class GUIBoardPosition : MonoBehaviour
{
    public bool mouseOver = false;
    public TextMeshPro _tmpro;
    public GUITextOutput _parent;
    public Tile _tile;

    public void UpdateTileText()
    {
        string text = _parent.EmptySymbol;
        if (_tile.hasEntities)
        {
            var thisEntity = _tile.GetEntity();
            switch (thisEntity.type)
            {
                case "trap":
                    text = _parent.TrapSymbol;
                    break;
                case "avatar":
                    text = _parent.AvatarDirectionSymbol[(thisEntity as Avatar).facingDirection];
                    break;
            }
        }
        _tmpro.text = text;
    }
    public void UpdateTileColor()
    {
        _tmpro.color = _tile.isActive ? _parent.ActiveTileColor : _parent.InactiveTileColor;
    }

    public void UpdateTile()
    {
        UpdateTileColor();
        UpdateTileText();
    }

    public void OnDataUpdated(object source, EventArgs args) => UpdateTile();

    private void OnMouseEnter() => mouseOver = true;
    private void OnMouseExit() => mouseOver = false;
}

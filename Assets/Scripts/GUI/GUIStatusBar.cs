using System;
using UnityEngine;
using TMPro;

public class GUIStatusBar : MonoBehaviour
{
    private TextMeshPro _tmpro;
    private RectTransform _rectTransform;

    private void UpdateText(GUIBoardPosition bp)
    {
        string contents = "";
        if (bp.Tile.hasEntities)
        {
            contents = ": ";
            foreach (Entity entity in bp.Tile.GetEntities())
            {
                if (contents != ": ") contents += ", ";
                contents += entity.name;
            }
        }
        
        _tmpro.text = $"({bp.x},{bp.y}){contents}";
    }

    protected virtual void OnMouseEnterTile(object source, EventArgs args)
    {
        var bp = source as GUIBoardPosition;
        UpdateText(bp);
        bp.BoardPositionDataUpdated += OnBoardPositionDataUpdated;
    }

    protected virtual void OnMouseExitTile(object source, EventArgs args)
    {
        var bp = source as GUIBoardPosition;
        bp.BoardPositionDataUpdated -= OnBoardPositionDataUpdated;
        _tmpro.text = "";
    }

    protected virtual void OnBoardPositionDataUpdated(object source, EventArgs args)
    {
        UpdateText(source as GUIBoardPosition);
    }

    private void Initialize()
    {
        _tmpro = gameObject.AddComponent<TextMeshPro>();
        _tmpro.fontSize = GUIManager.StatusBarFontSize;
        _tmpro.fontStyle = GUIManager.StatusBarFontStyle;
        _tmpro.alignment = GUIManager.StatusBarAlignment;

        _rectTransform = gameObject.GetComponent<RectTransform>();
        var camera = Camera.main;
        var screenBottomLeft = camera.ViewportToWorldPoint(Vector3.zero);
        var screenBottomRight = camera.ViewportToWorldPoint(Vector3.right);
        var sizeX = screenBottomRight.x - screenBottomLeft.x;
        _rectTransform.sizeDelta = new Vector2(sizeX, GUIManager.StatusBarHeight);
        _rectTransform.pivot = Vector2.zero;
        transform.position = new Vector3(screenBottomLeft.x, screenBottomLeft.y, 0);

        foreach (GameObject boardPosition in GUIManager.GetBoardPositions())
        {
            var gui = boardPosition.GetComponent<GUIBoardPosition>();
            gui.MouseEnterTile += OnMouseEnterTile;
            gui.MouseExitTile += OnMouseExitTile;
        }
    }

    void Start() => Initialize();
}

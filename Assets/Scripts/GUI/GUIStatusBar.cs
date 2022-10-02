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
            foreach (Entity entity in bp.Tile.GetEntities())
            {
                if (contents != "") contents += ", ";
                contents += entity.name;
            }
        }
        else contents = "Empty";

        _tmpro.text = $"({bp.x},{bp.y}): {contents}";
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
        transform.position = Vector2.zero;
        _tmpro = gameObject.AddComponent<TextMeshPro>();

        _tmpro.fontSize = GUIManager.StatusBarFontSize;
        _tmpro.fontStyle = GUIManager.StatusBarFontStyle;
        _tmpro.alignment = GUIManager.StatusBarAlignment;

        _rectTransform = gameObject.GetComponent<RectTransform>();
        _rectTransform.sizeDelta = GUIManager.StatusBarSize;

        foreach (GameObject boardPosition in GUIManager.GetBoardPositions())
        {
            var gui = boardPosition.GetComponent<GUIBoardPosition>();
            gui.MouseEnterTile += OnMouseEnterTile;
            gui.MouseExitTile += OnMouseExitTile;
        }
    }

    void Start() => Initialize();
}

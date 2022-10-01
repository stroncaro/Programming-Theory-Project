using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUIStatusBar : MonoBehaviour
{
    private bool _initialized = false;

    private TextMeshPro _tmpro;
    private RectTransform _rectTransform;

    private void Initialize()
    {
        transform.position = Vector2.zero;
        _tmpro = gameObject.AddComponent<TextMeshPro>();

        _tmpro.fontSize = GUIManager.StatusBarFontSize;
        _tmpro.fontStyle = GUIManager.StatusBarFontStyle;
        _tmpro.alignment = GUIManager.StatusBarAlignment;

        _rectTransform = gameObject.GetComponent<RectTransform>();
        _rectTransform.sizeDelta = GUIManager.StatusBarSize;

        _initialized = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        /*
        var thisScript = _tiles[x, y].GetComponent<GUIBoardPosition>();
        if (thisScript.mouseOver)
        {
            mouseOverTile = true;
            _statusBar.gameObject.SetActive(true);
            _statusBar.text = $"{thisTMPro.gameObject.name}";
        }
        */

        
    }
}

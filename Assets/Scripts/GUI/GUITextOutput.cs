using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUITextOutput : MonoBehaviour
{
    private TextMeshPro[,] _output;

    [SerializeField] private float _cellWidth = 0.5f;
    [SerializeField] private float _fontSize = 6;
    [SerializeField] private FontStyles _fontStyle;
    [SerializeField] private TextAlignmentOptions _alignment = TextAlignmentOptions.Center;
    [SerializeField] private bool _enableTextWrapping = false;

    [SerializeField] private Color _activeTileColor = Color.white;
    [SerializeField] private Color _inactiveTileColor = Color.black;

    private void Initialize()
    {
        _output = new TextMeshPro[GameData.Instance.board.rows, GameData.Instance.board.files];

        for (int x = 0; x < _output.GetLength(0); x++)
        {
            for (int y = 0; y < _output.GetLength(1); y++)
            {
                var newObj = new GameObject(string.Format("BoardPosition:{0},{1}", x, y));
                newObj.transform.position = new Vector2(x * _cellWidth, y * _cellWidth);
                newObj.transform.SetParent(transform);

                newObj.AddComponent<TextMeshPro>();
                var newObjTMPro = newObj.GetComponent<TextMeshPro>();
                newObjTMPro.fontSize = _fontSize;
                newObjTMPro.fontStyle = _fontStyle;
                newObjTMPro.alignment = _alignment;
                newObjTMPro.enableWordWrapping = _enableTextWrapping;
                newObjTMPro.text = "_"; //temporary

                var newObjRectTransform = newObj.GetComponent<RectTransform>();
                newObjRectTransform.sizeDelta = new Vector2(_cellWidth, _cellWidth);

                _output[x, y] = newObjTMPro;
            }
        }
    }

    void Start() => Initialize();

    private void UpdateOutput()
    {
        var board = GameData.Instance.board;
        for (int x = 0; x < board.rows; x++)
        {
            for (int y = 0; y < board.files; y++)
            {
                var thisTile = board.tiles[x, y];
                var thisTMPro = _output[x, y];
                thisTMPro.color = thisTile.isActive ? _activeTileColor : _inactiveTileColor;
                thisTMPro.text = thisTile.hasEntities ? string.Format("{0}", thisTile.entityIds[0]) : "_";
            }
        }
    }

    private void Update() => UpdateOutput();
}

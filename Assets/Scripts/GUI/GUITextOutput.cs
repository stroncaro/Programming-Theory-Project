using System;
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
    [SerializeField] private string _trapSymbol = "X";

    private Dictionary<BoardDirection, string> _avatarSymbols;

    private void Initialize()
    {
        _output = new TextMeshPro[GameData.Instance.board.rows, GameData.Instance.board.files];
        _avatarSymbols = new Dictionary<BoardDirection, string>();
        _avatarSymbols[BoardDirection.RIGHT] = "\u02C3";
        _avatarSymbols[BoardDirection.LEFT] = "\u02C2";
        _avatarSymbols[BoardDirection.UP] = "\u02C4";
        _avatarSymbols[BoardDirection.DOWN] = "\u02C5";

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

                string text = "_";
                if (thisTile.hasEntities)
                {
                    int thisEntityId = thisTile.entityIds[0];
                    var thisEntity = GameData.Instance.entities.Find(entity => entity.id == thisEntityId);
                    switch (thisEntity.type)
                    {
                        case "trap":
                            text = _trapSymbol;
                            break;
                        case "avatar":
                            //var avatar = thisEntity as Avatar;
                            text = _avatarSymbols[(thisEntity as Avatar).facing];
                            break;
                    }
                }
                thisTMPro.text = text;
            }
        }
    }

    private void Update() => UpdateOutput();
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUITextOutput : MonoBehaviour
{
    private GameObject[,] _output;

    [SerializeField] private float _cellWidth = 0.5f;
    [SerializeField] private float _fontSize = 6;
    [SerializeField] private FontStyles _fontStyle;
    [SerializeField] private TextAlignmentOptions _alignment = TextAlignmentOptions.Center;
    [SerializeField] private bool _enableTextWrapping = false;

    [SerializeField] private Color _activeTileColor = Color.white;
    [SerializeField] private Color _inactiveTileColor = Color.black;
    [SerializeField] private string _emptySymbol = "_";
    [SerializeField] private string _trapSymbol = "X";

    private Dictionary<Direction.World, string> _avatarDirectionSymbol = new Dictionary<Direction.World, string>
    {
        { Direction.World.NORTH , "\u02C4" },
        { Direction.World.EAST , "\u02C3" },
        { Direction.World.SOUTH , "\u02C5" },
        { Direction.World.WEST , "\u02C2" }
    };

    private TextMeshPro _statusBar;
    [SerializeField] private float _statusBarFontSize = 6;
    [SerializeField] private FontStyles _statusBarFontStyle;
    [SerializeField] private TextAlignmentOptions _statusBarAlignment;
    [SerializeField] private Vector2 _statusBarSize;

    public Color ActiveTileColor { get => _activeTileColor; }
    public Color InactiveTileColor { get => _inactiveTileColor; }
    public string EmptySymbol { get => _emptySymbol; }
    public string TrapSymbol { get => _trapSymbol; }
    public Dictionary<Direction.World, string> AvatarDirectionSymbol { get => _avatarDirectionSymbol; }

    private GameObject InitializeTile(int x, int y)
    {
        var newObj = new GameObject(string.Format("BoardPosition:{0},{1}", x, y));
        newObj.transform.position = new Vector2(x * _cellWidth, y * _cellWidth);
        newObj.transform.SetParent(transform);

        var newObjCollider = newObj.AddComponent<BoxCollider2D>();
        newObjCollider.size = new Vector2(0.5f, 0.5f);
        newObjCollider.offset = Vector2.zero;

        var newObjTMPro = newObj.AddComponent<TextMeshPro>();
        newObjTMPro.fontSize = _fontSize;
        newObjTMPro.fontStyle = _fontStyle;
        newObjTMPro.alignment = _alignment;
        newObjTMPro.enableWordWrapping = _enableTextWrapping;

        var newObjRectTransform = newObj.GetComponent<RectTransform>();
        newObjRectTransform.sizeDelta = new Vector2(_cellWidth, _cellWidth);

        var newObjScript = newObj.AddComponent<GUIBoardPosition>();
        newObjScript._tile = GameData.GetBoard().GetTile(x, y);
        newObjScript._parent = this;
        newObjScript._tmpro = newObjTMPro;
        newObjScript._tile.TileDataUpdated += newObjScript.OnDataUpdated;
        
        newObjScript.UpdateTile();

        return newObj;
    }

    private void InitializeTiles()
    {
        var board = GameData.GetBoard();
        _output = new GameObject[board.rows, board.files];

        for (int x = 0; x < board.rows; x++)
        {
            for (int y = 0; y < board.files; y++)
            {
                _output[x, y] = InitializeTile(x, y);
            }
        }
    }

    private void InitializeStatusBar()
    {
        var newObj = new GameObject("StatusBar");
        newObj.transform.position = Vector2.zero;
        newObj.transform.SetParent(transform);

        var newObjTMPro = newObj.AddComponent<TextMeshPro>();
        newObjTMPro.fontSize = _statusBarFontSize;
        newObjTMPro.fontStyle = _statusBarFontStyle;
        newObjTMPro.alignment = _statusBarAlignment;

        var newObjRectTransform = newObj.GetComponent<RectTransform>();
        newObjRectTransform.sizeDelta = _statusBarSize;

        _statusBar = newObjTMPro;
    }

    private void Initialize()
    {
        InitializeTiles();
        InitializeStatusBar();
    }

    void Start() => Initialize();

    private void UpdateOutput()
    {
        bool mouseOverTile = false; //keep track if the mouse is over a tile

        var board = GameData.GetBoard();
        for (int x = 0; x < board.rows; x++)
        {
            for (int y = 0; y < board.files; y++)
            {
                //update tile
                var thisTile = board.GetTile(x, y);
                var thisTMPro = _output[x, y].GetComponent<TextMeshPro>();
                thisTMPro.color = thisTile.isActive ? _activeTileColor : _inactiveTileColor;

                string text = "_";
                if (thisTile.hasEntities)
                {
                    var thisEntity = thisTile.GetEntity();
                    switch (thisEntity.type)
                    {
                        case "trap":
                            text = _trapSymbol;
                            break;
                        case "avatar":
                            text = _avatarDirectionSymbol[(thisEntity as Avatar).facingDirection];
                            break;
                    }
                }
                thisTMPro.text = text;

                //update status bar text
                var thisScript = _output[x, y].GetComponent<GUIBoardPosition>();
                if (thisScript.mouseOver)
                {
                    mouseOverTile = true;
                    _statusBar.gameObject.SetActive(true);
                    _statusBar.text = $"{thisTMPro.gameObject.name}";
                }
            }
        }

        if (!mouseOverTile) _statusBar.gameObject.SetActive(false);
    }

    //private void Update() => UpdateOutput();
}

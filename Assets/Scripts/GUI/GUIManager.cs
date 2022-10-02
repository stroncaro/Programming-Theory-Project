using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUIManager : MonoBehaviour
{
    private static GUIManager _instance;

    private GameObject[,] _boardPositions;
    public static GameObject[,] GetBoardPositions() { return _instance._boardPositions; }
    [SerializeField] private float _cellWidth = 0.5f;
    public static float CellWidth { get => _instance._cellWidth; }
    [SerializeField] private float _fontSize = 6;
    public static float FontSize { get => _instance._fontSize; }
    [SerializeField] private FontStyles _fontStyle;
    public static FontStyles FontStyle { get => _instance._fontStyle; }
    [SerializeField] private TextAlignmentOptions _alignment = TextAlignmentOptions.Center;
    public static TextAlignmentOptions FontAlignment { get => _instance._alignment; }
    [SerializeField] private bool _enableTextWrapping = false;
    public static bool EnableTextWrapping { get => _instance._enableTextWrapping; }

    [SerializeField] private Color _activeTileColor = Color.white;
    public static Color ActiveTileColor { get => _instance._activeTileColor; }
    [SerializeField] private Color _inactiveTileColor = Color.black;
    public static Color InactiveTileColor { get => _instance._inactiveTileColor; }
    [SerializeField] private string _emptySymbol = "_";
    public static string EmptySymbol { get => _instance._emptySymbol; }
    [SerializeField] private string _trapSymbol = "X";
    public static string TrapSymbol { get => _instance._trapSymbol; }

    private Dictionary<Direction.World, string> _avatarDirectionSymbol = new Dictionary<Direction.World, string>
    {
        { Direction.World.NORTH , "\u02C4" },
        { Direction.World.EAST , "\u02C3" },
        { Direction.World.SOUTH , "\u02C5" },
        { Direction.World.WEST , "\u02C2" }
    };
    public static Dictionary<Direction.World, string> AvatarDirectionSymbol { get => _instance._avatarDirectionSymbol; }

    private GameObject _statusBar;
    [SerializeField] private float _statusBarFontSize = 6;
    public static float StatusBarFontSize { get => _instance._statusBarFontSize; }
    [SerializeField] private FontStyles _statusBarFontStyle;
    public static FontStyles StatusBarFontStyle { get => _instance._statusBarFontStyle; }
    [SerializeField] private TextAlignmentOptions _statusBarAlignment;
    public static TextAlignmentOptions StatusBarAlignment { get => _instance._statusBarAlignment; }
    [SerializeField] private float _statusBarHeight;
    public static float StatusBarHeight { get => _instance._statusBarHeight; }

    private GameObject CreateBoardPosition(int x, int y)
    {
        var newObj = new GameObject($"BoardPosition:{x},{y}");
        newObj.transform.SetParent(transform);
        var newObjScript = newObj.AddComponent<GUIBoardPosition>();
        newObjScript.SetTileCoordinates(x, y);

        return newObj;
    }

    private GameObject[,] CreateBoardPositions()
    {
        var board = GameData.GetBoard();
        var tiles = new GameObject[board.rows, board.files];

        for (int i = 0; i < board.rows * board.files; i++)
        {
            int x = i / board.files;
            int y = i % board.files;
            tiles[x, y] = CreateBoardPosition(x, y);
        }

        return tiles;
    }

    private GameObject CreateStatusBar()
    {
        var newObj = new GameObject("StatusBar");
        newObj.transform.SetParent(transform);
        var newObjScript = newObj.AddComponent<GUIStatusBar>();

        return newObj;
    }

    private void CenterCameraOnBoard()
    {
        var board = GameData.GetBoard();
        float x = _cellWidth * (board.rows -1) / 2;
        float y = _cellWidth * (board.files -1) / 2;
        float z = -10;

        var camera = Camera.main;
        camera.transform.position = new Vector3(x, y, z);
    }

    private void Initialize()
    {
        if (_instance != null)
        {
            Debug.LogWarning("There can be only one GUIManager");
            return;
        }

        _instance = this;
        _boardPositions = CreateBoardPositions();
        _statusBar = CreateStatusBar();

        CenterCameraOnBoard();
    }

    void Start() => Initialize();
}

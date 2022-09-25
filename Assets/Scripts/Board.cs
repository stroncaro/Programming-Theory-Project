using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Tile tileActivePrefab;
    [SerializeField] private Tile tileInactivePrefab;

    private Tile[,] _tileMap  = new Tile[10, 10];
    private int _bordersCut = 1;

    public int bordersCut
    {
        get => _bordersCut;
        set
        {
            _bordersCut = value;
            DestroyBoard();
            CreateBoard();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateBoard();
    }

    private void CreateBoard()
    {
        for (int x = 0; x < _tileMap.GetLength(0); x++)
        {
            for (int y = 0; y < _tileMap.GetLength(1); y++)
            {
                var prefab = PointInsidePlayArea(x, y) ? tileActivePrefab : tileInactivePrefab;

                Tile newTile = Instantiate(prefab);
                newTile.transform.position = new Vector2(x, y);
                newTile.transform.SetParent(this.transform);
                newTile.gameObject.name = string.Format("Tile({0},{1})", x, y);
                newTile.colorIndex = (x + y) % 2;

                _tileMap[x, y] = newTile;
            }
        }
    }

    private void DestroyBoard()
    {
        foreach (var tile in _tileMap)
        {
            Destroy(tile);
        }
    }

    private bool PointInsidePlayArea(int x, int y)
    {
        return x >= _bordersCut &&
            x <= _tileMap.GetLength(0) - _bordersCut -1 &&
            y >= _bordersCut &&
            y <= _tileMap.GetLength(1) - _bordersCut -1;
    }
}

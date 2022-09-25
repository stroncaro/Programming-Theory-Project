using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Tile tilePrefab;

    //colors for tiles, should be moved out later into a UI class or something
    [SerializeField] private Color activeLightColor;
    [SerializeField] private Color activeDarkColor;
    [SerializeField] private Color inactiveLightColor;
    [SerializeField] private Color inactiveDarkColor;

    //make visible in inspector temporarily
    private Tile[,] tileMap  = new Tile[9, 9];

    // Start is called before the first frame update
    void Start()
    {
        //init tiles
        for (int x = 0; x < tileMap.GetLength(0); x++)
        {
            for (int y = 0; y < tileMap.GetLength(1); y++)
            {
                Tile newTile = Instantiate(tilePrefab);
                newTile.transform.SetParent(this.transform);
                newTile.gameObject.name = string.Format("Tile({0},{1})", x, y);
                newTile.color = (x + y) % 2 == 0 ? activeLightColor : activeDarkColor;
                tileMap[x,y] = newTile;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Tile tileActivePrefab;
    [SerializeField] private Tile tileInactivePrefab;


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
                Tile newTile = Instantiate(tileActivePrefab);
                newTile.transform.position = new Vector2(x, y);
                newTile.transform.SetParent(this.transform);
                newTile.gameObject.name = string.Format("Tile({0},{1})", x, y);
                newTile.colorIndex = (x + y) % 2;
                
                tileMap[x,y] = newTile;
            }
        }
    }
}

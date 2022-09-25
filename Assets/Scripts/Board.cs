using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Tile tileActivePrefab;
    [SerializeField] private Tile tileInactivePrefab;

    private Tile[,] tileMap  = new Tile[10, 10];
    private float bordersCut = 1;

    // Start is called before the first frame update
    void Start()
    {
        //init tiles
        for (int x = 0; x < tileMap.GetLength(0); x++)
        {
            for (int y = 0; y < tileMap.GetLength(1); y++)
            {
                var tileCheck =
                    x < bordersCut ||
                    y < bordersCut ||
                    x > tileMap.GetLength(0) - bordersCut -1 ||
                    y > tileMap.GetLength(1) - bordersCut -1;
                var prefab = tileCheck ? tileInactivePrefab : tileActivePrefab;

                Tile newTile = Instantiate(prefab);
                newTile.transform.position = new Vector2(x, y);
                newTile.transform.SetParent(this.transform);
                newTile.gameObject.name = string.Format("Tile({0},{1})", x, y);
                newTile.colorIndex = (x + y) % 2;
                
                tileMap[x,y] = newTile;
            }
        }
    }
}

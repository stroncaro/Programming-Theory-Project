using UnityEngine;

public class Board
{
    //number of rows and files determine size of board
    private int _rows, _files;
    public int rows { get => _rows; }
    public int files { get => _files; }
    
    //in each coordinate there is a tile
    private Tile[,] _tiles;

    public Tile GetTile(int x, int y) => _tiles[x, y];
    public Tile GetTile(Vector2 coordinates) => GetTile((int)coordinates.x, (int)coordinates.y);

    public Board(int rows, int files)
    {
        //set board size
        _rows = rows;
        _files = files;

        //create board of proper width and height
        _tiles = new Tile[_rows, _files];

        //initialize tiles
        for (int i = 0; i < _rows * _files; i++) { _tiles[i / _files, i % _files] = new Tile(); }
    }
}

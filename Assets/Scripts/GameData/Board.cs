public enum BoardDirection { UP, RIGHT, DOWN, LEFT }

public class Board
{
    //number of rows and files determine size of board
    private int _rows, _files;
    public int rows { get => _rows; }
    public int files { get => _files; }
    
    //in each coordinate there is a tile
    private Tile[,] _tiles;
    public Tile[,] tiles { get => _tiles; }

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
